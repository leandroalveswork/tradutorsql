Public Class MainWindow 
    Public Property ComandoSql As String
    Public Property CodigoCSharp As String
    Public Property NomeDaVariavel As String
    Private _naoEAtualizacaoDosDemaisEstados As Boolean

    Public Sub New()
        InitializeComponent()
        ComandoSql = ""
        CodigoCSharp = ""
        NomeDaVariavel = ""
        _naoEAtualizacaoDosDemaisEstados = False
    End Sub

    Public Sub InputSql_TextChanged(sender As Object, args As EventArgs)
        If _naoEAtualizacaoDosDemaisEstados Then
            Return
        End If
        ComandoSql = InputSql.Text
        NomeDaVariavel = InputNomeDaVariavel.Text
        _naoEAtualizacaoDosDemaisEstados = True
        InputCSharp.Text = ConverterParaCSharp(ComandoSql, NomeDaVariavel)
        _naoEAtualizacaoDosDemaisEstados = False
    End Sub

    Private Function ConverterParaCSharp(ByVal comandoSql As String, ByVal nomeDaVariavel As String) As String
        If nomeDaVariavel Is Nothing Or nomeDaVariavel.Equals("") Then
            Return ""
        End If
        Dim linhasSql() As String = comandoSql.Split(Environment.NewLine)
        Dim totalCSharp As String = ""
        For Each linhaSql As String In linhasSql
            Dim comandoSqlEscapado As String = linhaSql.Replace("""", "\""")
            totalCSharp &= nomeDaVariavel & ".AppendLine(""" & comandoSqlEscapado + """);" & Environment.NewLine
        Next
        Return totalCSharp
    End Function

    Public Sub InputCSharp_TextChanged(sender As Object, args As EventArgs)
        If _naoEAtualizacaoDosDemaisEstados Then
            Return
        End If
        CodigoCSharp = InputCSharp.Text
        _naoEAtualizacaoDosDemaisEstados = True
        InputSql.Text = ConverterParaSql(CodigoCSharp)
        _naoEAtualizacaoDosDemaisEstados = False
    End Sub

    Private Function ConverterParaSql(ByVal codigoCSharp As String) As String
        Dim linhasDeCodigo() As String = codigoCSharp.Split(Environment.NewLine)
        Dim totalSql As String = ""
        For Each linhaDeCodigo As String In linhasDeCodigo
            Dim pedacosEntreAsAspas() As String = linhaDeCodigo.Split("""")
            Dim idxPedaco As Integer = 0
            Dim comandoSqlExtraido As String = ""
            For Each pedacoEntreAspas As String In pedacosEntreAsAspas
                If (idxPedaco <> 0 And idxPedaco <> pedacosEntreAsAspas.Length - 1) Then
                    comandoSqlExtraido &= pedacoEntreAspas & """"
                End If
                idxPedaco += 1
            Next
            If (comandoSqlExtraido.Length > 0) Then
                comandoSqlExtraido = comandoSqlExtraido.Substring(0, comandoSqlExtraido.Length - 1).Replace("\""", """")
            End If
            totalSql &= comandoSqlExtraido & Environment.NewLine
        Next
        Return totalSql
    End Function

    Public Sub InputNomeDaVariavel_TextChanged(sender As Object, args As EventArgs)
        If _naoEAtualizacaoDosDemaisEstados Then
            Return
        End If
        ComandoSql = InputSql.Text
        NomeDaVariavel = InputNomeDaVariavel.Text
        _naoEAtualizacaoDosDemaisEstados = True
        InputCSharp.Text = ConverterParaCSharp(ComandoSql, NomeDaVariavel)
        _naoEAtualizacaoDosDemaisEstados = False
    End Sub

    Public Sub BotaoLimpar_Click(sender As Object, args As RoutedEventArgs)
        ComandoSql = ""
        CodigoCSharp = ""
        NomeDaVariavel = ""
        _naoEAtualizacaoDosDemaisEstados = True
        InputSql.Text = ""
        InputCSharp.Text = ""
        InputNomeDaVariavel.Text = ""
        _naoEAtualizacaoDosDemaisEstados = False
    End Sub
End Class
