/*
author: Sean Mooney | 40283592
spell: checks spelling of input data against dictionary
last modified: 27/02/2019
*/

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "spellCheck.h"


int inputSwitch;
int outputSwitch;
int caseSwitch;

char* inputFile;
char* outputFile;

char** dictionary;
int dictionaryWords = 0;

char** wordstore;
int wordCount = 0;


void processCommandArgs(int argc, char **argv) 
{	
	if (argc <= 2)
	{
		printf("Not enough command arguments\n");
		exit(0);
	}
	else if (argc > 2)
	{
		for (int i = 1; i < argc; ++i)
		{
			if (strcmp(argv[i], "-i") == 0)
			{
				if (strcmp(argv[i+1], "-o") != 0)
				{
					//check if string is a .txt file
					printf("Input file selected\n");
					inputFile = argv[i+1];
					inputSwitch = 1;
					++i;
				}
				else
				{
					printf("No input file entered. Input will be taken from stdin\n");
				}
			}
			else if (strcmp(argv[i], "-o") == 0)
			{
				if (argv[i+1] == NULL || argv[i+1] == "-c")
				{
					printf("No output file selected. Output will go to stdout\n\n");
				}
				else
				{
					//check if string is a .txt file
					printf("Output file selected\n\n");
					outputFile = argv[i+1];
					outputSwitch = 1;
					++i;
				}
			}
			else if (strcmp(argv[i], "-c") == 0)
			{
				printf("Ignore case of input\n");
				caseSwitch = 1;
			}
			else
			{
				printf("unknown input: %s. terminating", argv[i]);
				exit(0);
			}

		}

	}

}

void remove_Newline(char *str)
{
	int len = strlen(str);
	
	if (len > 0 && str[len - 1] == '\n')
	{
		str[len - 1] = '\0';
	}
}

void remove_Spaces(char *str)
{
	int len = strlen(str);
	
	if (len > 0 && str[len - 1] == ' ')
	{
		str[len - 1] = '\0';
	}
}

void readDictionary()
{//reads and stores local dictionary.txt file
	FILE *file;
	file = fopen("dictionary.txt", "r");
	
	char* word;
	int size = 25;
	int index = 1;
	int c = 0;
	
	while(1) 
	{//count each line
		c = fgetc(file);
		
		if (c == '\n')
		{
			index++;
		}
		if(feof(file))
		{ 
			break ;
		}
	}
	rewind(file);
	
	//create dictionary word store
	dictionary = (char**)malloc(index * sizeof(char*));
	index = 0;

	while(!feof(file))
	{//read each line of the dictionary and store each word in store
		word = (char*)calloc(1, size);
		fgets(word, 25, file);
		if (strlen(word) > 0 && strcmp(word, "\n") != 0)
		{
			remove_Newline(word);
			remove_Spaces(word);
			dictionary[index] = word;
			index++;
		}
	}
	dictionaryWords = index;
	
	fclose(file);
}

void sentenceBreak(char *str, int line)
{//takes each passed line of sentence file and stores as individual words
	int word_start = 0;
	int word_end = 0;
	int count = 0;
	char *word;
	
	char lineNum[3];
	char lyne[11] = "@line: ";

	itoa(line, lineNum, 10);
	
	strcat(lyne, lineNum);
	
	printf("|%s|\n", lyne);
	
	for (int i = 0; i <= strlen(str); i++)
	{	
		if(str[i] == ' ' || str[i] == '\n') 
		{
			word_start = word_end;
			count = 0;
			wordstore[wordCount] = word;
			wordCount++;
			word = NULL;
		}
		else if(word_start == word_end)
		{
			word = (char*) malloc(30);
			word[count] = str[i];
			word_end++;
			count++;
		}
		else if(str[i] == '.' && str[i] == str[strlen(str)-1])
		{
			word_start = word_end;
			count = 0;
			wordstore[wordCount] = word;
			wordCount++;
			word = NULL;
		}
		else if(str[i] == '.' || str[i] == ',' || str[i] == '?')
		{
			word[count] = 0;
			count++;
		}
		else
		{
			word[count] = str[i];
			word_end++;
			count++;
		}
	}
}

enum CHOICE
{
	QUIT = 0,
	WORDS = 1,
	LENGTH = 2
};

void readInput()
{//reads words from stdin or given input .txt file
	int c = 0;
	int word_size = 25;
	int case_size = 1200;
	int index = 1;
	int caseCheck;
	char *word;
	char *sentence;
	char *case_test;
	
	if(!inputSwitch)
	{//if no input file is given store single word from stdin
		int flag = 1;
		int state_flag = 0;
		int state;
		
		while (flag)
		{	
			printf("\nPlease enter a word for spell checking\nEnter 'q' to quit\n");
			char* single_word = (char*)calloc(1, 30);
			fgets(single_word, 30, stdin);
			remove_Newline(single_word);
			
			if(strcmp(single_word, "q") == 0)
			{
				state = 0;
				state_flag = 1;
			}
			else if(strlen(single_word) > 25)
			{
				state = 2;
				state_flag = 1;
			}
			else
			{
				for(int i = 0; i < strlen(single_word); i++)
				{
					if(single_word[i] == ' ')
					{
						state = 1;
						state_flag = 1;
					}
				}
			}
			
			if(!state_flag)
			{
				wordstore = (char**)malloc(1 * sizeof(char*));
				wordstore[0] = single_word;
				wordCount = 1;
				break;
			}
		
			switch (state)
			{
				case WORDS:
					printf("\nPlease enter only one word!\n");
					state_flag = 0;
					break;
				case LENGTH:
					printf("\nWords over 25 characters cannot be processed!\n");
					break;
				case QUIT:
					flag = 0;
					caseCheck = 2;
					printf("\nexiting....goodbye\n");
					exit(0);
				default:
					printf("*** INVALID INPUT ***\n");
					break;
			}
		}
	}
	else
	{//if input file is given store first line of input file
		FILE *file;
		file = fopen(inputFile, "r");
			
		case_test = (char*)calloc(1, case_size);
		fgets(case_test, 1200, file);
		rewind(file);
		
		if(strlen(case_test) <= 25)
		{//if length of first line is less or equal to 25 then assume single word input file	
			while(1) 
			{//count number of lines for input file
				c = fgetc(file);
				
				if (c == '\n')
				{
					index++;
				}
				if(feof(file))
				{ 
					break ;
				}
			}
			rewind(file);
			//create wordstore for input file data
			wordstore = (char**)malloc(index * sizeof(char*));
			index = 0;
		
			while(!feof(file))
			{//read each line of input and store per line word
				word = (char*)calloc(1, word_size);
				fgets(word, 25, file);
				if((strlen(word) > 0) && (strcmp(word, "\n") != 0))
				{//store words unless lines are not null but only contain newline char
					wordstore[index] = word;
					remove_Newline(wordstore[index]);
					remove_Spaces(wordstore[index]);
					wordCount++;
					index++;
				}
			}
			wordCount = index;
		}
		else if(strlen(case_test) > 25)
		{//if length of first line is greater than 25 then assume sentence input file
			while(1) 
			{//read file to get word count
				c = fgetc(file);
				
				if ((c == ' ') || (c == EOF))
				{
					index++;
				}
				else if (c == '\n')
				{
					c++;
				}
				else if(c == '.' || c == ',' || c == '?')
				{
					c++;
				}
				if(feof(file))
				{ 
					index++;
					break;
				}
			}
			rewind(file);
			
			//create wordstore to size of word count
			wordstore = (char**)malloc(index * sizeof(char*));
			int line = 1;
			
			while(!feof(file))
			{//pass each line as a string to sentence_Break method
				sentence = (char*) calloc(1, case_size);
				fgets(sentence, 1200, file);
				if(strcmp(sentence, "\n") != 0)
				{
					sentenceBreak(sentence, line);
					line++;
				}
			}
		}
		fclose(file);
	}
}



int main(int argc, char **argv) 
{
	printf("Welcome to %s\n******************\n", argv[0]);
	
	processCommandArgs(argc, argv);
	readDictionary();
	readInput();
	
	spellCheck(dictionary, wordstore, dictionaryWords, wordCount, caseSwitch);
	
	
	for (int i = 0; i <= wordCount; i++)
	{
		wordstore[i] = NULL;
		free(wordstore[i]);
	}
	
	for (int i = 0; i <= dictionaryWords; i++)
	{
		dictionary[i] = NULL;
		free(dictionary[i]);
	}
	
	return 0;

}