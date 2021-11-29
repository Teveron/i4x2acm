## i4x2wav

I wrote a small program to split up an .i4x file into its individual .acm files and then convert them to .wav files.  The .wav files are stored in a folder, and you have the option to keep the .acm files as well.  Once decoded, the files are numbered from 0.  Since Starfleet Command uses dynamic music, only some parts are combined into a song in-game, so I decided against combining the .wav files into a single one.  The dynamic music uses a file called “SFA  Jumplists_Tabs.txt” to combine the parts together, but I haven’t been able to decode it yet.

## Dependencies

You’ll need to download “acm2wav.exe” which is not included because it’s not my program and I haven’t asked permission to include it.  Google it, and it’ll be one of the first entries.  You’ll also need a audio editor like Audacity to combine the .wav files into one.

## Usage

First, download the folder “i4x2wav” from GitHub.  You’ll need the entire folder and its contents.  Please don’t rename or delete any files within.  Double-click on “i4x2acm.exe” to start.  Don't click on "i4x2wav.exe.config" or "i4x2wav.pdb" by mistake.  It won't hurt your computer if you do, it just won't do anything.

Second, download “acm2wav.exe”, then click on “Locate acm2wav” and navigate to wherever you saved it.  Once you find it the first time, it’ll be copied so you don’t have to find it every time.  

Next, click on “Select File To Convert” and select your .i4x file.  Only one file can be converted at a time.

Then click on “Select Save To Location” and select a folder to save the converted .acm and .wav files to.  Remember, there will be multiple files generated.

Tick the check box “Keep acm Files” if you want them.  If not, they’ll automatically be deleted after conversion.

Lastly, click “Convert”.  If you’re converting multiple files, remember to change the save to location for subsequent files.  



