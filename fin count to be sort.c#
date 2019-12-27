//input:int array
// you need to find one continuous subarray that will be sort then whole array will be sorted in asc order 
//outpu:find the shortest subarray length
using System;
using System.Collections;
					
public class Program
{
	public static void Main()
	{
		int[] a = {1,2,8,5,7,6};
		int[] b = new int[a.Length];
		int count = 0;
		for(int i= 1;i<=a.Length;i++)
		{
			for(int j = i+1; j <=a.Length -1; j++){
				
				if (a[i] > a[j])
				{
					count++;
				}
				
			}
		}
		Console.Write("count value= " + count);
		
		

	}
}
