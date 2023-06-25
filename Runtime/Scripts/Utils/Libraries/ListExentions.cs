// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Provides extension methods for List objects.
    /// </summary>
    public static class ListExtensions
    {
        private static readonly Random _random = new Random();
        
        #region Methods

        #region Generic

        /// <summary>
        /// Returns the first element of the list.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <returns>The first element of the list.</returns>
        public static T First<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            return list[0];
        }

        /// <summary>
        /// Returns the last element of the list.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <returns>The last element of the list.</returns>
        public static T Last<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            return list[list.Count - 1];
        }

        /// <summary>
        /// Prints the non-null elements of the list to the console.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        public static void PrintOnConsole<T>(this List<T> list)
        {
            foreach (var item in list)
            {
                Debug.Log(item.ToString());
            }
        }

        /// <summary>
        /// Converts the list to a queue.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <returns>A queue containing the elements of the list.</returns>
        public static Queue<T> ListToQueue<T>(this List<T> list)
        {
            return new Queue<T>(list);
        }

        #region Random Elements

        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <returns>A random element from the list.</returns>
        public static T RandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            int index = _random.Next(list.Count);
            return list[index];
        }

        /// <summary>
        /// Returns a random element from the list, excluding the specified value.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <param name="excludedValue">The value to exclude.</param>
        /// <returns>A random element from the list, excluding the specified value.</returns>
        public static T RandomElementExcept<T>(this List<T> list, T excludedValue)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            List<T> eligibleElements = list.FindAll(item => !item.Equals(excludedValue));

            if (eligibleElements.Count == 0)
            {
                throw new InvalidOperationException("No eligible elements found in the list.");
            }

            int index = _random.Next(eligibleElements.Count);
            return eligibleElements[index];
        }

        /// <summary>
        /// Returns a random element from the list, excluding the elements in the specified list.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <param name="excludedValues">The list of values to exclude.</param>
        /// <returns>A random element from the list, excluding the elements in the specified list.</returns>
        public static T RandomElementExcept<T>(this List<T> list, List<T> excludedValues)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            List<T> eligibleElements = list.FindAll(item => !excludedValues.Contains(item));

            if (eligibleElements.Count == 0)
            {
                throw new InvalidOperationException("No eligible elements found in the list.");
            }

            int index = _random.Next(eligibleElements.Count);
            return eligibleElements[index];
        }

        #endregion

        #region Sorting Methods

        /// <summary>
        /// Sorts the list using the Insertion Sort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list to be sorted.</param>
        public static void InsertionSort<T>(this List<T> list) where T : IComparableObject
        {
            int n = list.Count; // Number of elements in the list

            for (int i = 1; i < n; i++) // Iterate over the list starting from the second element
            {
                T key = list[i]; // Current element to be inserted at the correct position
                int j = i - 1; // Index of the previous element

                // Compare the key with each element before it and shift them to the right if they are greater
                while (j >= 0 && list[j].GreaterThan(key))
                {
                    list[j + 1] = list[j]; // Shift the element to the right
                    j--;
                }

                list[j + 1] = key; // Insert the key at the correct position
            }
        }

        /// <summary>
        /// Sorts the list using the Selection Sort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list to be sorted.</param>
        public static void SelectionSort<T>(this List<T> list) where T : IComparableObject
        {
            int n = list.Count; // Number of elements in the list

            for (int i = 0;
                 i < n - 1;
                 i++) // Iterate over the list from the first element to the second-to-last element
            {
                int minIndex = i; // Index of the minimum element

                // Find the index of the minimum element in the remaining unsorted part of the list
                for (int j = i + 1; j < n; j++)
                {
                    if (list[j].LessThan(list[minIndex]))
                    {
                        minIndex = j;
                    }
                }

                // Swap the current element with the minimum element
                if (minIndex != i)
                {
                    T temp = list[i];
                    list[i] = list[minIndex];
                    list[minIndex] = temp;
                }
            }
        }

        /// <summary>
        /// Sorts the list using the Bubble Sort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list to be sorted.</param>
        public static void BubbleSort<T>(this List<T> list) where T : IComparableObject
        {
            int n = list.Count; // Number of elements in the list

            for (int i = 0;
                 i < n - 1;
                 i++) // Iterate over the list from the first element to the second-to-last element
            {
                bool swapped = false; // Flag to track if any swaps occurred in the current iteration

                // Compare adjacent elements and swap them if they are in the wrong order
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].GreaterThan(list[j + 1]))
                    {
                        // Swap the elements
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;

                        swapped = true; // Set the swapped flag to true
                    }
                }

                // If no swaps occurred in the current iteration, the list is already sorted
                // and we can exit the loop
                if (!swapped)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Sorts the list using the Merge Sort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list to be sorted.</param>
        public static void MergeSort<T>(this List<T> list) where T : IComparable
        {
            MergeSort(list, 0, list.Count - 1);
        }

        // Recursive method to perform Merge Sort
        private static void MergeSort<T>(List<T> list, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(list, left, middle);
                MergeSort(list, middle + 1, right);
                Merge(list, left, middle, right);
            }
        }

        // Merge two subarrays of list[]
        private static void Merge<T>(List<T> list, int left, int middle, int right) where T : IComparable
        {
            // Sizes of the two subarrays to be merged
            int size1 = middle - left + 1;
            int size2 = right - middle;

            // Create temporary arrays
            T[] leftArray = new T[size1];
            T[] rightArray = new T[size2];

            // Copy data to temporary arrays
            for (int i = 0; i < size1; ++i)
            {
                leftArray[i] = list[left + i];
            }

            for (int j = 0; j < size2; ++j)
            {
                rightArray[j] = list[middle + 1 + j];
            }

            // Merge the temporary arrays

            // Initial indexes of the first and second subarrays
            int leftIndex = 0;
            int rightIndex = 0;

            // Initial index of the merged subarray
            int mergedIndex = left;

            while (leftIndex < size1 && rightIndex < size2)
            {
                if (leftArray[leftIndex].CompareTo(rightArray[rightIndex]) <= 0)
                {
                    list[mergedIndex] = leftArray[leftIndex];
                    leftIndex++;
                }
                else
                {
                    list[mergedIndex] = rightArray[rightIndex];
                    rightIndex++;
                }

                mergedIndex++;
            }

            // Copy the remaining elements of leftArray[], if any
            while (leftIndex < size1)
            {
                list[mergedIndex] = leftArray[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            // Copy the remaining elements of rightArray[], if any
            while (rightIndex < size2)
            {
                list[mergedIndex] = rightArray[rightIndex];
                rightIndex++;
                mergedIndex++;
            }
        }

        #endregion

        #endregion

        #region SpecificListType

        /// <summary>
        /// Finds the corresponding element of type T1 in the list based on the specified element of type T.
        /// </summary>
        /// <typeparam name="T">The type of the first value in the BiClass.</typeparam>
        /// <typeparam name="T1">The type of the second value in the BiClass.</typeparam>
        /// <param name="list">The list of BiClass instances.</param>
        /// <param name="element">The element to find a corresponding value for.</param>
        /// <returns>The corresponding element of type T1 from the list.</returns>
        public static T1 FindCorrespondingElementTo<T, T1>(this List<BiClass<T, T1>> list, T element)
        {
            T1 elementToFind = default(T1);

            foreach (BiClass<T, T1> listElement in list)
            {
                if (EqualityComparer<T>.Default.Equals(listElement.firstValue, element))
                {
                    elementToFind = listElement.secondValue;
                }
            }

            return elementToFind;
        }

        /// <summary>
        /// Finds the corresponding element of type T1 in the list based on the specified element of type T.
        /// </summary>
        /// <typeparam name="T1">The type of the first value in the BiClass.</typeparam>
        /// <typeparam name="T">The type of the second value in the BiClass.</typeparam>
        /// <param name="list">The list of BiClass instances.</param>
        /// <param name="element">The element to find a corresponding value for.</param>
        /// <returns>The corresponding element of type T1 from the list.</returns>
        public static T1 FindCorrespondingElementTo<T1, T>(this List<BiClass<T1, T>> list, T element)
        {
            T1 elementToFind = default(T1);;

            foreach (BiClass<T1, T> listElement in list)
            {
                if (EqualityComparer<T>.Default.Equals(listElement.secondValue, element))
                {
                    elementToFind = listElement.firstValue;
                }
            }
            
            return elementToFind;
        }
        
        /// <summary>
        /// Converts a list of characters to a string.
        /// </summary>
        /// <param name="charList">The list of characters.</param>
        /// <returns>The resulting string.</returns>
        public static string ToString(this List<char> charList)
        {
            return new string(charList.ToArray());
        }

        /// <summary>
        /// Converts a List of BiClass<T, T1> objects to a Dictionary<T, T1>.
        /// </summary>
        /// <typeparam name="T">The type of the key in the dictionary.</typeparam>
        /// <typeparam name="T1">The type of the value in the dictionary.</typeparam>
        /// <param name="biClassList">The List of BiClass<T, T1> objects to convert.</param>
        /// <returns>A Dictionary<T, T1> containing the converted key-value pairs.</returns>
        public static Dictionary<T, T1> ToDictionary<T, T1>(this List<BiClass<T, T1>> biClassList)
        {
            Dictionary<T, T1> dictionary = new Dictionary<T, T1>();

            foreach (BiClass<T, T1> biClass in biClassList)
            {
                dictionary.Add(biClass.firstValue, biClass.secondValue);
            }

            return dictionary;
        }
        
        /// <summary>
        /// Converts a Dictionary<T, T1> to a List of BiClass<T, T1> objects.
        /// </summary>
        /// <typeparam name="T">The type of the key in the dictionary.</typeparam>
        /// <typeparam name="T1">The type of the value in the dictionary.</typeparam>
        /// <param name="dictionary">The Dictionary<T, T1> to convert.</param>
        /// <returns>A List of BiClass<T, T1> objects containing the key-value pairs from the dictionary.</returns>
        public static List<BiClass<T, T1>> ToBiClassList<T, T1>(this Dictionary<T, T1> dictionary)
        {
            List<BiClass<T, T1>> biClassList = new List<BiClass<T, T1>>();

            foreach (KeyValuePair<T, T1> pair in dictionary)
            {
                BiClass<T, T1> biClass = new BiClass<T, T1>(pair.Key, pair.Value);
                biClassList.Add(biClass);
            }

            return biClassList;
        }

        #endregion
        
        #endregion
    }
}