import sys
import itertools
import math
import os

# get file from cmd line
file_name = sys.argv[1] + ".cav"

with open(file_name) as f:
    read_data = f.read()

f.close()

# comma separate read data
map = read_data.split(",")

# get number of caves from first element
caves = int(map[0])

unvisited_nodes = []
connectivity = []

# get caves coordinates based on caves*2
count = 0
cave_count = 0
temp = []
for i in itertools.islice(map, 1, caves*2+1):
    if count == 0:
        temp.append(cave_count)
        temp.append(int(i))
        count = 1
        cave_count += 1
    elif count == 1:
        temp.append(int(i))
        if len(unvisited_nodes) == 0:
            temp.append(0)
        else:
            temp.append(math.inf)
        unvisited_nodes.append(temp)
        count = 0
        temp = []

# get connectivity matrix based on caves*caves
for i in itertools.islice(map, caves*2+1, (caves*caves)+(caves*2+1)):
    if count < caves-1:
        temp.append(int(i))
        count += 1
    elif count == caves-1:
        temp.append(int(i))
        connectivity.append(temp)
        count = 0
        temp = []

visited_nodes = []

# run djikstra's algorithm on nodes
last_found = 0
while last_found == 0:
    smol = min(unvisited_nodes, key=lambda x: x[3])
    for i in range(caves):
        if int(connectivity[i][smol[0]]) == 1:
            unvisted_iter = 0
            for n in unvisited_nodes:
                if i == n[0]:
                    euclidean_distance = math.sqrt((smol[1] - n[1])**2 + (smol[2] - n[2])**2)
                    new_dist = euclidean_distance + smol[3]
                    if new_dist < unvisited_nodes[unvisted_iter][3]:
                        unvisited_nodes[unvisted_iter][3] = new_dist
                        if len(unvisited_nodes[unvisted_iter]) == 4:
                            unvisited_nodes[unvisted_iter].append(smol[0])
                        elif len(unvisited_nodes[unvisted_iter]) == 5:
                            unvisited_nodes[unvisted_iter][4] = smol[0]
                unvisted_iter += 1
    unvisted_iter = 0
    for n in unvisited_nodes:
        if n == smol:
            visited_nodes.append(unvisited_nodes.pop(unvisted_iter))
            if n[0] + 1 == caves and len(n) == 5:
                last_found = 1
            #if len(n) == 6:
                #print(n)
        unvisted_iter += 1
    unvisited_nodes.sort(key = lambda x: x[3])

visited_nodes.reverse()

output = open("solution.csn","w+")

# if last cave found then backtrack to get path or output 0 if not found
if last_found == 1:
    route = []
    dist = 0
    step = caves
    for leaf in visited_nodes:
        if leaf[0] + 1 == step:
            route.append(leaf[0] + 1)
            if leaf[0] != 0:
                step = leaf[4] + 1
            if dist == 0:
                dist = round(leaf[3], 2)
            route.sort()
    output.write("Path: {}\nLength: {}".format(route, dist))
elif last_found == 0:
    output.write("0")

output.close()
