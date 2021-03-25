# Interplay i4x

I4x files are music files used in Starfleet Command.  They are essentially containers for multiple acm files.  Starfleet Command joins these acm files in different ways to create dynamic music.

An i4x file has 2 main parts to it, what I call an i4x header, and then a number of acm files.  The acm files aren’t further encrypted or encoded, so we can just copy the bytes to a new file and have a working acm file.

The i4x header has 2 parts as well.  The first 16 bytes are about the i4x file, and then there is information about the acm files contained in the i4x file.  Words are usually 4 bytes and the file is little endian.

##### i4x header:  (all sizes in bytes)

| Offset | Size | Description |
| --- | --- | --- |
| 0 | 4 | 0x10 0x04 0x10 0x02 |
| 4 | 4 | Sample rate, usually 22050 |
| 8 | 4 | Unknown, possibly size of decompressed audio |
| 12 | 4 | Count of acm files contained in this file |

##### acm file info:  (all sizes in bytes)

| Size | Description |
| --- | --- |
| 4 | Unknown, possibly rolling length of decompressed files |
| 4 | Start of file |
| 4 | Length of file |
| 4 | Unknown, possibly length of decompressed file |


## Example

Here are the first 80 bytes from the file FedSucc.i4x:

```
10 04 10 02 22 56 00 00 30 3D 5A 00 2E 00 00 00
00 00 00 00 50 04 00 00 04 88 00 00 84 36 02 00 
84 36 02 00 54 8C 00 00 E2 68 00 00 A4 9B 01 00
28 D2 03 00 36 F5 00 00 AC 70 00 00 50 B2 01 00
78 84 05 00 E2 65 01 00 01 71 00 00 8C BC 01 00
```

The first 4 bytes are `0x10 0x04 0x10 0x02`.  Technically these are read one byte at a time, so I guess they also contain version information.  Next is `0x0005622` (remember, it’s little endian) which is 22050, the sample rate.  After that is `0x005a3d30` whih is 5913904.  I believe that’s the total length of the decompressed audio for all the acm files contained in this file.  Lastly is `0x0000002e` which is 46.  This means that there are 46 acm files in this i4x file.

The next line contains information about the first acm file.  It translates to 0, 1104, 34820, 145028.  That means that there’s 0 bytes of previous audio (because this is the first file), this acm file starts at 1104, this acm file is 34820 bytes long compressed, and it’s 145028 bytes decompressed.  The data for this file is located from 1104 to 35923 (1104 + 34820 - 1).

The third line is about the second acm file.  It translates to 145028, 35924, 26850, 105380.  The previous audio is 145028 bytes long (the length of the previous file, which is the only previous audio).  This acm file starts at 35924, is 26850 bytes compressed, and 105380 bytes uncompressed.  Data for this file is located from 35924 to 250407 (35924 + 26850 - 1).

## i4x2acm

I wrote a small program to split up an i4x file into the individual acm files.  I then ran `acm2wav` on them using a script.  I originally combined the individual wav files into one, but a user on Reddit reminded me that the music was supposed to be dynamic, so I decided against including it here.

To convert i4x files to acm files, use `i4x2acm.exe` from the command line.  It takes one argument, a string that can either be the path to a single i4x file, or to a folder containing multiple i4x files.  It will check the file extension to determine if a file is an i4x file or not.  For each file it finds it will create a folder named after the file (minus the extension) and create the necessary acm files.  The acm files will be named with a number, indicating which file it was in source order.  

Afterwards, use the batch file `acm2wav.bat` to run `acm2wav.exe` (almost the same name) on all of the acm files in the folder, again from the command line.  `Acm2wav.bat` also takes one argument, a path to a folder containing acm files.  

## Dependencies

Don’t forget that you’ll need `acm2wav.exe` which is not included.  Google it, and it’ll be one of the first entries.  You’ll also need a program like Audacity to combine the files into one.