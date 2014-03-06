using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using MvvMSample.Models;

namespace MvvMSample.ViewModels
{
    public class ChampionshipBetViewModel : IChampionshipBetViewModel, IDropTarget
    {
        public ChampionshipBetViewModel(IChampionship championship)
        {
            FootballClubs = championship.UserBet;
        }

        public ObservableCollection<IFootballClub> FootballClubs { get; private set; }

        public void DragOver(IDropInfo dropInfo)
        {
            var selectedIndices = this.GetItemsBlock(dropInfo.Data).Select(c => FootballClubs.IndexOf(c)).ToList();
            //important: InsertIndex is the index of the item right AFTER the position we are inserting into
            //consequently the range is within (both included) 0 and Items.Count
            if (selectedIndices.Any() && !selectedIndices.Contains(dropInfo.InsertIndex))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var targetIndex = dropInfo.InsertIndex;
            var selectedItems = this.GetItemsBlock(dropInfo.Data);
            if (!selectedItems.Any()) return;

            var sourceIndices = selectedItems.Select(c => FootballClubs.IndexOf(c)).ToArray();
            var sourceMinIndex = sourceIndices.Min();

            if (targetIndex < sourceMinIndex || targetIndex > sourceMinIndex + 1)
            {
                if (targetIndex < sourceMinIndex)
                {
                    for (int i = 0; i < sourceMinIndex - targetIndex; i++)
                    {
                        sourceIndices = MoveAllLeft(FootballClubs, sourceIndices);
                    }

                }
                else if (targetIndex > sourceMinIndex + 1)
                {
                    for (int i = 0; i < targetIndex - sourceMinIndex - 1; i++)
                    {
                        sourceIndices = MoveAllRight(FootballClubs, sourceIndices);
                    }
                }
            }
        }

        //drag and drop only allows insertion of contiguous blocks...
        private IEnumerable<IFootballClub> GetItemsBlock(object data)
        {
            if (data is IFootballClub)
            {
                return new[] { (IFootballClub)data };
            }
            else if (data is IEnumerable<IFootballClub>)
            {
                var block = ((IEnumerable<IFootballClub>)data).ToArray();
                var sortedIndices = block.Select(c => FootballClubs.IndexOf(c)).OrderBy(i => i).ToArray();
                if (IsANonDiscontiguousInterval(sortedIndices))
                {
                    return (IEnumerable<IFootballClub>)data;
                }

                return Enumerable.Empty<IFootballClub>();
            }
            else
            {
                return Enumerable.Empty<IFootballClub>();
            }
        }



        private static int[] MoveAllLeft<T>(ObservableCollection<T> collection, int[] sortedIndices)
        {
            var intervals = GetIntervals<T>(sortedIndices);

            var newPositions = new List<int>();
            MoveAllLeft(collection, intervals, newPositions);
            return newPositions.OrderBy(i => i).ToArray();
        }

        private static int[] MoveAllRight<T>(ObservableCollection<T> collection, int[] sortedIndices)
        {
            var intervals = GetIntervals<T>(sortedIndices);
            //start by reversing queue.
            var reverseIntervals = new Queue<int[]>();
            while (intervals.Count > 0)
            {
                reverseIntervals.Enqueue(intervals.Dequeue());
            }

            var newPositions = new List<int>();
            MoveAllRight(collection, reverseIntervals, newPositions);
            return newPositions.OrderBy(i => i).ToArray();
        }

        private static Queue<int[]> GetIntervals<T>(int[] sortedIndices)
        {
            var intervals = new Queue<int[]>();
            int i = 0;
            while (i < sortedIndices.Length)
            {
                int j = i;
                while (j < sortedIndices.Length)
                {
                    if (sortedIndices[i] + (j - i) == sortedIndices[j])
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
                intervals.Enqueue(new int[] { sortedIndices[i], sortedIndices[j - 1] });
                i = j;
            }
            return intervals;
        }

        private static void MoveAllLeft<T>(ObservableCollection<T> collection, Queue<int[]> intervals, List<int> newPositions)
        {
            if (intervals.Count == 0) return;
            var group = intervals.Dequeue();

            int start = group[0];
            int end = group[1];

            if (start != 0)
            {
                for (int i = start; i <= end; i++)
                {
                    collection.Move(i, i - 1);
                    newPositions.Add(i - 1);
                }
            }
            else
            {
                newPositions.AddRange(Enumerable.Range(start, end - start + 1));
            }

            MoveAllLeft(collection, intervals, newPositions);
        }

        private static void MoveAllRight<T>(ObservableCollection<T> collection, Queue<int[]> intervals, List<int> newPositions)
        {
            if (intervals.Count == 0) return;
            var group = intervals.Dequeue();

            int start = group[0];
            int end = group[1];

            if (end != collection.Count - 1)
            {
                for (int i = end; i >= start; i--)
                {
                    collection.Move(i, i + 1);
                    newPositions.Add(i + 1);
                }
            }
            else
            {
                newPositions.AddRange(Enumerable.Range(start, end - start + 1));
            }

            MoveAllRight(collection, intervals, newPositions);
        }

        /// <summary>
        /// Returns true if the sequences is of the form (e.g. 3,4,5,6 etc.)
        /// Returns false if there is a hole (e.g. 3,6,7,8 etc.)
        /// </summary>
        /// <param name="sortedIndices">sorted array of integer</param>
        /// <returns></returns>
        private static bool IsANonDiscontiguousInterval(int[] sortedIndices)
        {
            for (int i = 0; i < sortedIndices.Length - 1; i++)
            {
                if (sortedIndices[i + 1] > sortedIndices[i] + 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
