.PHONY : report clear main open

report : main

main : ./tex/MakefileTex
	cd ./tex && make -f MakefileTex

open :
	xdg-open pdf/rpz.pdf

clear :
	rm -rf ./pdf/ ./out/
	cd ./tex && make -f MakefileTex clear
