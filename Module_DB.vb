Module Module_DB
    Public cnsqlclient As New System.Data.SqlClient.SqlConnection
    Public source, catalog, uid, pwd As String

    Public Function DB_INIT()
        Dim sr As System.io.StreamReader = New System.IO.StreamReader("bicdb.ini")
        Dim line As String
        Dim line_len As Integer
        Dim eq_pos As Integer
        Dim line_key As String
        Do
            line = sr.ReadLine()
            line_len = Len(line)
            If line_len = 0 Then
            Else
                eq_pos = InStr(1, line, "=", 1)
                line_key = Mid(line, 1, eq_pos - 1)
                Select Case line_key
                    Case Is = "source"
                        source = Mid(line, eq_pos + 1, line_len - eq_pos)
                    Case Is = "catalog"
                        catalog = Mid(line, eq_pos + 1, line_len - eq_pos)
                    Case Is = "uid"
                        uid = Mid(line, eq_pos + 1, line_len - eq_pos)
                    Case Is = "pwd"
                        pwd = Mid(line, eq_pos + 1, line_len - eq_pos)
                End Select
            End If
        Loop Until line Is Nothing
        sr.Close()
    End Function

    Public Function DB_OPEN() As Boolean
        DB_OPEN = False
        '*****  �ڑ���������쐬���Đڑ����J�n����  *****
        If Trim(uid) <> Nothing Then
            cnsqlclient.ConnectionString = "integrated security=false; uid=" & uid & "; pwd=" & pwd & "; data source=" & source & ";persist security info=False; initial catalog=" & catalog
        Else
            cnsqlclient.ConnectionString = "integrated security=SSPI;data source=" & source & ";persist security info=False;initial catalog=" & catalog
        End If

        Try
            '*****  Connection���ڑ�����Ă��邩�`�F�b�N  *****
            If cnsqlclient.State = ConnectionState.Closed Then
                cnsqlclient.Open()
            End If
        Catch
            MsgBox(Err.Description, 16, "�ڑ��G���[")
            DB_OPEN = False
            Exit Function
        End Try

        DB_OPEN = True
    End Function

    Public Sub DB_CLOSE()
        cnsqlclient.Close()     '�ڑ����I������
    End Sub
End Module
