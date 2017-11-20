#!/bin/python3

import sys

def FiF(nmax, modulus):
    fif = [[0] * (nmax + 1) for i in range(2)]
    fif[0][0] = 1
    for i in range(1, nmax + 1):
        fif[0][i] = int((fif[0][i - 1] * i) % modulus)
    a, b = fif[0][nmax], modulus
    p, q = 1, 0
    while b > 0:
        c, d = a // b, a
        a, b = b, d % b
        d = p
        p, q = q, d - c * q
    fif[1] = [0] * (nmax + 1)
    fif[1][nmax] = int((p + modulus) if p < 0 else p)
    for i in range(nmax - 1, -1, -1):
        fif[1][i] = int((fif[1][i + 1] * (i + 1)) % modulus)
    return fif

def Choose(n, r, fif, modulus):
    if n < 0 or r < 0 or r > n: return 0
    return int((((fif[0][n] * fif[1][r]) % modulus) * fif[1][n - r]) % modulus)

def Mul(a, b, modulus):
    return (a * b) % modulus

def Div(a, b, modulus):
    return (a * Inv(b, modulus)) % modulus

def Inv(a, modulus):
    b = modulus
    p, q = 1, 0
    while b > 0:
        c, d = a // b, a
        a, b = b, d % b
        d = p
        p, q = q, d - c * q
    return (p + modulus) if p < 0 else p

def Ber(M, R):
    FIF = FiF(M + 1, R)
    
    B = [0] * M;
    B[0] = 1
    for i in range(1, M):
        b = 0
        for j in range(i):
            b = (b + Div(Mul(Choose(i, j, FIF, R), B[j], R), i - j + 1, R)) % R
        B[i] = int((1 + R - b) % R)
    return B

def highwayConstruction(n, K, B, R):
    sum = 0
    if n > 2:
        b, n1 = 1, (n - 1) % R
        for k in range(K, -1, -1):
            b = Mul(Div(Mul(b, k + 1, R), K - k + 1, R), n1, R)
            sum = (sum + Mul(b, B[k], R)) % R
        sum = (R + Div(sum, K + 1, R) - 1) % R
    return sum

if __name__ == "__main__":
    M = 1010
    R = 1000000009
    B = Ber(M, R)
    
    q = int(input().strip())
    for a0 in range(q):
        n, k = input().strip().split(' ')
        n, k = [int(n), int(k)]
        result = highwayConstruction(n, k, B, R)
        print(result)
