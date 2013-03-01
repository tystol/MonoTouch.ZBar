BTOUCH=/Developer/MonoTouch/usr/bin/btouch
all: ZBar.dll

libzbar.a:
	@echo "You'll need to obtain a copy of libzbar.a"
	@echo "from http://zbar.sourceforge.net/iphone/"
	@echo
	@open http://zbar.sourceforge.net/iphone/
	exit 1

ZBar.dll: Makefile libzbar.a
	@echo "TODO"
	exit 1
	$(BTOUCH) ApiDefinition.cs AssemblyInfo.cs

clean:
	-rm -f *.a *.dll
