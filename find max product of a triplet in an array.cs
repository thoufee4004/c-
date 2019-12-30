//given an integer array find the max product of a triplet in array
//ip:[10,3,5,6,20]
//:1200
//ip:[1,-4,3,-6,7,0]
//op:168

using System;
					
public class Program
{
	public static void Main()
	{
		Console.WriteLine("enter the array size");
		 int i=Convert.ToInt32(Console.ReadLine());
		 int[] arr=new int[i];
		int a=i;
		int j;
		for( j=0;j<a;j++)
		{
			arr[j]=Convert.ToInt32(Console.ReadLine());
			if(arr[j]<0)
			{
				arr[j]=arr[j]*(-1);
			}
 		}
		Array.Sort(arr);
		Console.WriteLine("array values ",arr);
		for( j=0;j<a;j++)
		{
		Console.Write( + arr[j] +","); 
		}
		Console.Write("\nthe maximum product of the triplet in array is=");
		int curr_sum =arr[j-1]*arr[j-2]*arr[j-3];
		Console.Write(+curr_sum);
	}}
