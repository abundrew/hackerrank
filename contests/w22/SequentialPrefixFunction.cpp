#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
#include <algorithm>
using namespace std;

int main() {
    string str;
    getline(cin, str);
    int Q = atoi(str.c_str());
    int S[Q] = {0};
    int ix = 0;
    int LPS[Q] = {0};
    int LEN[Q] = {0};
    int len = 0;
    
    for (int q = 0; q < Q; q++) {
        getline(cin, str);
        switch (str[0]) {
            case '+':
                S[ix] = atoi(str.substr(2).c_str());
                if (ix > 0) {
                    while (true) {
                        if (S[ix] == S[len])
                        {
                            len++;
                            LPS[ix] = len;
                            break;
                        }
                        else
                        {
                            if (len != 0)
                            {
                                len = LPS[len - 1];
                            }
                            else
                            {
                                LPS[ix] = 0;
                                break;
                            }
                        }
                    }
                }
                LEN[ix] = len;
                cout << LPS[ix] << endl;
                ix++;
                break;
            case '-':
                ix--;
                len = ix > 0 ? LEN[ix - 1] : 0;
                cout << len << endl;
                break;
        }
    }
    return 0;
}
