#!/bin/bash

#Author: Sean Mooney 40283592
#Version: 1.0
#Last updated: 03/12/2017

#script will sort found photo files in a directory, check  and copy to a new achive directory


#declared var holds type of file to search for
declare -r search_files=IMG_[0-9][0-9][0-9][0-9].JPG
#check health of user input
for i do
#if args are invalid (too many, wrong format, missing) display error message
if [ $# -lt 2 ] ; then echo "phar takes 2 arguments eg. phar sort_directory into_archive"
	elif [ $# -eq 3 ] ; then echo "Too many command args" ; exit
	elif [ ! -d $1 ] ; then echo "Directory for sorting doesn't exist" ; exit
	#if inputs correct capture list of paths to files create archive dir
	#line_count controls the head of the search so we are not rechecking files
	else find $1 -name "*$search_files" -type f > $1/file_paths.txt ; mkdir -p $2 ; line_count=1
#store file path at head
for image_path in `cat $1/file_paths.txt` ; do file_name=$(basename "$image_path" .deb) ; echo CHECKING $image_path
	#move head of search by one
	duplicate_flag=0 ; line_count=$((line_count+1));
	#check file name against every other file name in file paths list
	while IFS= read -r file_line ; do file_name2=$(basename "$file_line" .deb)
		#compare files
		if [ "$file_name" == "$file_name2" ] ; then
		#if file is duplicate, do not copy and record absolute path in 'duplicates.txt'
		if cmp -s $image_path $file_line ; then	echo ...$file_name is duplicate, not copied ; echo $image_path >> $2/duplicates.txt ; duplicate_flag=1
		#if not unique name, but not duplicate file, copy to directory, append with .JPG
		else cp -v -- "$image_path" "$2/$file_name.JPG" ; duplicate_flag=1 ; fi ; fi ; done	< <(tail -n +$line_count $1/file_paths.txt)
			#if unique, copy to new directory
			if [ "$duplicate_flag" == 0 ] ; then cp $image_path $2 | echo ...$file_name copied to $2 ; fi ; done ; fi ; done
#if it exists delete file containing file paths of original files
if [ -f "$1/file_paths.txt" ]; then rm $1/file_paths.txt ; fi