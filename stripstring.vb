    Sub Main()

        Dim arg() As String = Environment.GetCommandLineArgs()

        Dim sFile As String = ""
        Dim sStringStart As String = ""
        Dim iReplaceAscii As Integer = 120
        Dim iStartPos As Integer = 1

        If UBound(arg) = 0 Then
            Console.WriteLine("Stripstring, written in 2021 by Glenn Larsson.")
            Console.WriteLine("Freeware, made to work with the first 1000 header bytes. Does not do unicode. CC0.")
            Console.WriteLine("Syntax: stripstring <filename> " & Chr(34) & "Case sensitive string" & Chr(34) & " <replacement ascii code> <starting offset/position>" & vbCrLf)
            End
        End If

        Try
            sFile = arg(1)
            sStringStart = arg(2)
            iReplaceAscii = CInt(arg(3))
            iStartPos = CInt(arg(4))
        Catch ex As Exception
        End Try

        If iReplaceAscii > 255 Then
            Console.WriteLine("Warning: Replacement ascii code > 255, defaulting to 120 (x)" & vbCrLf)
            iReplaceAscii = 120 ' default
        End If

        If Dir(sFile).Trim = "" Then
            Console.WriteLine("Critical: file '" & sFile & "' does not exist - stopping." & vbCrLf)
            End
        End If

        If FileLen(sFile) < 1000 Then
            Console.WriteLine("Critical: file needs to be longer than 1K - stopping." & vbCrLf)
            End
        End If

        If iStartPos = 0 Then iStartPos = 1
        If iStartPos >= FileLen(sFile) Then
            Console.WriteLine("Critical: Arbitrary start position cant be > filelen - stopping." & vbCrLf)
            End
        End If

        'If InStr(1, LCase(sFile), ".pcap") = 0 Then
        '    Console.WriteLine("Critical: No .pcap/.pcapng extension?- stopping." & vbCrLf)
        '    End
        'End If

        Dim bFile() As Byte = System.IO.File.ReadAllBytes(sFile)
        Dim bHeader(1000) As Byte

        For n = 0 To 999
            bHeader(n) = bFile(n)
        Next

        Dim sBuildString As String = System.Text.Encoding.Default.GetString(bHeader)
        Dim iPos As Integer = InStr(iStartPos, sBuildString, sStringStart) - 1

        If iPos > 0 Then
            Console.WriteLine("String available at position: " & iPos)
            Dim bBreak As Boolean = False
            Console.Write(Chr(34))
            Do
                Console.Write(Chr(bHeader(iPos)))
                bFile(iPos) = iReplaceAscii
                If bHeader(iPos + 1) = 0 Then
                    bBreak = True
                    Console.Write(Chr(34))
                End If

                iPos = iPos + 1
            Loop While bBreak = False
        End If

        Try
            System.IO.File.WriteAllBytes(sFile, bFile)
            Console.WriteLine(vbCrLf & "Wrote: " & sFile & vbCrLf)
        Catch ex As Exception
        End Try

    End Sub
