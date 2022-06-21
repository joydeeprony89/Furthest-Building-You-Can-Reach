using System;
using System.Collections.Generic;

namespace Furthest_Building_You_Can_Reach
{
  public class MaxPQ : IComparer<int>
  {
    public int Compare(int a, int b)
    {
      if (b > a) return 1;
      else if (a > b) return -1;
      else return 0;
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Solution s = new Solution();
      var heights = new int[] { 4, 12, 2, 7, 3, 18, 20, 3, 19 };
      int bricks = 10;
      int ladders = 2;
      var answer = s.FurthestBuilding(heights, bricks, ladders);
    }
  }

  public class Solution
  {
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
      // We will use bricks first
      // after utilizing all the bricks when we have to use ladder that time we will try to optimize th previous max diff with the ladder and the recent one with 
      PriorityQueue<int, int> pq = new PriorityQueue<int, int>(new MaxPQ());
      int i = 0;
      for (; i < heights.Length - 1; i++)
      {
        if (heights[i + 1] <= heights[i]) continue;
        int diff = heights[i + 1] - heights[i];

        // we always try to use bricks first
        // if we have enough bricks to satisfy current need
        if (bricks >= diff)
        {
          bricks -= diff;
          pq.Enqueue(diff, diff);
        }
        else if (ladders > 0)
        { // when we have consumed all the bricks, will be using ladder now
          if (pq.Count > 0)
          {
            // get the maximum bricks which we have used previously
            // instead of using bricks here will be using ladder as it is more convinient, we can use the bricks for latter climbings
            int max = pq.Peek();
            if (max > diff)
            {
              // get back the previously used bricks
              // will be using ladder for the previous jump, line no 29
              bricks += max;
              pq.Dequeue();
              // will be using bricks for current jump
              bricks -= diff;
              // we are swapping previous max bricks used jump with the ladder and current jimp with bricks
              pq.Enqueue(diff, diff);
            }
          }
          // will be using ladders 
          ladders--;
        }
        else
          break;
      }

      return i;
    }
  }
}
