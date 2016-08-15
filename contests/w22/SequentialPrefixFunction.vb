Imports System
Imports System.IO
Module Module1

    Sub Main()

        Dim tIn As TextReader = Console.In
        Dim tOut As TextWriter = Console.Out

        Dim nQ As Integer = Integer.Parse(tIn.ReadLine())

        Dim S(nQ + 1) As Integer
        Dim ix As Integer = 0

        Dim LPS(nQ + 1) As Integer
        Dim LEN(nQ + 1) As Integer
        Dim alen As Integer = 0

        For q As Integer = 0 To nQ - 1
            Dim line As String = tIn.ReadLine()
            Select Case line(0)
                Case "+"c
                    S(ix) = Integer.Parse(line.Substring(2))

                    If ix > 0 Then
                        While True
                            If (S(ix) = S(alen)) Then
                                alen += 1
                                LPS(ix) = alen
                                Exit While
                            Else
                                If alen <> 0 Then
                                    alen = LPS(alen - 1)
                                Else
                                    LPS(ix) = 0
                                    Exit While
                                End If
                            End If
                        End While
                    End If
                    LEN(ix) = alen
                    tOut.WriteLine(LPS(ix))
                    ix += 1
                Case "-"c
                    ix -= 1
                    alen = If(ix > 0, LEN(ix - 1), 0)
                    tOut.WriteLine(alen)
            End Select
        Next
    End Sub

End Module
