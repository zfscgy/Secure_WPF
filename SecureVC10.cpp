#include "stdafx.h"
#include <iostream>
#include <fstream>
#include <string>
#include <time.h>
#include <Windows.h>
using namespace std;
#pragma once
int RandomN = 0;
int seed1, seed2, seed3;
int Random()
{
	RandomN = (seed1*RandomN + seed2*seed2 / (RandomN + 10)) % seed3;
	return RandomN % 256;
}
int Encrypt(ifstream &Scan,ofstream &Encode)
{
	char c;
	char encryptedC;
	int charInt;
	while (!Scan.eof())
	{
		Scan.read(&c, sizeof(char)); charInt = c;
		encryptedC = (charInt + Random()) % 256;
		if (!Scan.eof())
		{
			Encode.write(&encryptedC, sizeof(char));
		}

	}
	cout << "Complete!!!\n\n";
	return 0;
}
int Decrypt(ifstream &Scan, ofstream &Encode)
{
	char c;
	char encryptedC;
	int charInt;
	while (!Scan.eof())
	{
		Scan.read(&c, sizeof(char)); charInt = c;
		encryptedC = (charInt - Random()) % 256;
		if (!Scan.eof())
		{
			Encode.write(&encryptedC, sizeof(char));
		}

	}
	cout << "Complete!!!\n\n";
	return 0;
}
int Report(string fileName2, int choice,float totalTime)
{
	string mode;
	
	SYSTEMTIME currentT;
	GetLocalTime(&currentT);
	mode = (choice == 1) ? "Encrypted" : "Eecrypted";
	ofstream Print(fileName2+"_Report.txt");
	Print << mode << " " << "filename:" << fileName2 << endl;
	Print << "The key integer is " << seed1 << " " << seed2 << " " << seed3 << endl;
	Print << "Elapsed time:" << totalTime << "s" << endl;
	Print << "Date: " << currentT.wYear << "/" << currentT.wMonth << "/" << currentT.wDay << " " << currentT.wHour << ":" << currentT.wMinute << endl;
	Print.close();
	return 0;
}
int _tmain(int argc, _TCHAR* argv[])
{
	string fileName1, fileName2;
	int choice;
	cout << "This program is powered by Zheng Fei." << endl;
	cout << "_________________________________________" << endl;
	//for (;;)
	{
		cout << "1.Encrypt a file   2.Decrypt a file  3.Exit" << endl;
		//choice = atoi((char*)argv[1]);
		cin >> choice;
		if (choice == 3)  return 0;
		cout << "Put this program in the same directory with original file." << endl;
		cout << "Input the filename of original file, make sure the name is right." << endl;
		//fileName1 = (char*)argv[2];
		cin >> fileName1;		
		ifstream Scan(fileName1, ifstream::binary);
		if (Scan.bad())
		{
			cerr << "The file does not exist, check the filename\n";
			//continue;
		}
		cout << "Input the filename of the target file, make sure the name is not used in current directory" << endl;
		//fileName2 = (char*)argv[3];
		cin >> fileName2;
		ofstream Encode(fileName2, ofstream::binary);
		cout << "Input 3 integers as Encryption/Decryption key(4 digits  4 digits   6 digits),use space to sperate." << endl << "Example 1000 1000 100000 [Enter]" << endl;
		//seed1 = atoi((char*)argv[4]);seed2 = atoi((char*)argv[5]);seed3 = atoi((char*)argv[6]);
		cin >> seed1 >> seed2 >> seed3;
		float time1 = clock();
		
		if (choice == 1)  Encrypt(Scan, Encode);
		else if (choice == 2)  Decrypt(Scan, Encode);
		else
		{
			cerr << "Input error!" << endl;
			//continue;
		}
        float time2 = clock();
		float totalT = (time2 - time1) / CLOCKS_PER_SEC;
		Report(fileName2,choice,totalT);
		RandomN = 0;
	}
    return 0;
}

