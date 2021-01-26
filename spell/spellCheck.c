#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "spellCheck.h"

void spellCheck(char** dictionary, char** wordstore, int dictionaryWords, int wordCount, int caseSwitch)
{
	/*if (caseSwitch == 1)
	{
		convert 
	}*/
	for (int i = 0; i < wordCount; ++i)
	{
		int first = 0;
		int last = dictionaryWords - 1;
		int middle = (first+last/2);
		
		while (first <= last)
		{
			if (strcmp(dictionary[middle], wordstore[i]) < 0)
			{
				first = middle + 1;
			}
			else if (strcmp(dictionary[middle], wordstore[i]) == 0)
			{
				//printf("found word: %s...location: %d\n", wordstore[i], middle);
				break;
			}
			else
			{
				last = middle - 1;
			}
			middle = (first + last)/2;
		}
		if (first > last)
		{
			printf("Not found! '%s' isn't present in the list.\n", wordstore[i]);
		}	
		
	}
	
} 
