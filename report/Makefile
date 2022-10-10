.PHONY : report clear main

report : main clear

main : ./tex/MakefileTex
	cd ./tex && make -f MakefileTex

clear :
	rm -rf tex/_minted-rpz* tex/*.aux tex/*.bcf tex/*.bbl tex/*.blg tex/*.log tex/*.out tex/*.pdf tex/*.run.xml tex/*.toc tex/*.fdb_latexmk tex/*.fls tex/*.synctex.gz out*
