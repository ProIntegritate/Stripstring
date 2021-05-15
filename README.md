# Stripstring
Tool to strip specfic strings from the header (first 1000 bytes) of files given an input. I wrote this to rid PCAP files of metadata.

Syntax: stripstring <filename> "Case sensitive string" <replacement ascii code> <starting offset/position>
  
If you want to remove the Operating system info (in my case "64-bit Windows 10 (2009), build 19042") from a .PCAP file, you would use:

Stripstring.exe t.pcap "64-bit " 

And it would search and replace the first occurrance of that string, then exit. If you want to replace something a bit further in the file, you need to specify an offset.

Stripstring.exe t.pcap "64-bit " 120 300

(120 ("x") is the default character and a number need to be entered, i can't be arsed to faff around with parameter switches for such a small project, consult your local ASCII table for more.)

The filename specified will be overwritten, so it's up to you to keep backups if you screw something up.

source: Stripstring.vb
binary: Stripstring.zip
