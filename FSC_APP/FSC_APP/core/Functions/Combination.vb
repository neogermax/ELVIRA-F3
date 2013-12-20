
Public Class Combination : Implements IComparer(Of List(Of Object))

#Region " Combinatoria "

    ''' <summary>
    ''' Obtiene las combinaciones unicas anidadas
    ''' </summary>
    ''' <param name="items">Lista de las combinaciones</param>
    ''' <param name="k">El tamaño del subconjunto</param>
    Public Function GetSubsets(ByVal items() As Object, ByVal k As Integer) As List(Of List(Of Object))

        'Definicion de objetos
        Dim i As Integer = k
        Dim n As Integer = items.Length

        If (i - 1) < 0 OrElse k > n Then
            'Lanza excepción si el tamaño del subconjunto es inválido
            Throw New ArgumentOutOfRangeException(" El tamaño del subconjunto debe ser entre 1 to " & CStr(n))
        End If
        Dim finalList As New List(Of List(Of Object))
        i -= 1
        'añade los objetos a la primea sublista.
        Dim indexs(k - 1) As Integer
        Dim firstSubList As New List(Of Object)
        For j As Integer = 0 To k - 1
            indexs(j) = j
            firstSubList.Add(items(j))
        Next
        'añade la primera sub lista a la lista padre.
        finalList.Add(firstSubList)
        ' añade los ultimos
        While indexs(0) <> n - k AndAlso finalList.Count < 2147483647
            If indexs(i) < i + (n - k) Then
                indexs(i) += 1
                Dim subList As New List(Of Object)
                For Each j As Integer In indexs
                    'añade objetos a la sublista
                    subList.Add(items(j))
                Next
                ' añade la sublista a la lista padre
                finalList.Add(subList)
            Else
                Do
                    i -= 1
                Loop While indexs(i) = i + (n - k)
                indexs(i) += 1
                For j As Integer = i + 1 To k - 1
                    indexs(j) = indexs(j - 1) + 1
                Next
                Dim subList As New List(Of Object)
                For Each j As Integer In indexs
                    'añade los objetos a la sublista
                    subList.Add(items(j))
                Next
                'añade la sublista a la lista padre
                finalList.Add(subList)
                i = k - 1
            End If
        End While
        Return finalList
    End Function

    ''' <summary>
    ''' Obtiene las combinaciones unicas anidadas
    ''' </summary>
    ''' <param name="items">Lista de las combinaciones</param>
    ''' <param name="k">El tamaño del subconjunto</param>
    Public Function GetSubsets2(ByVal items() As Object, ByVal k As Integer) As List(Of String)

        Dim i As Integer = k
        Dim n As Integer = items.Length
        If (i - 1) < 0 OrElse k > n Then
            'Invalid subset size. Throw an exception.
            Throw New ArgumentOutOfRangeException( _
            "k", k, "The value must be in the range {1 to " & CStr(n) & "}.")
        End If
        Dim finalPairList As New List(Of String)
        i -= 1
        'añade los objetos a la primea sublista.
        Dim indexs(k - 1) As Integer
        Dim firstPair As String = ""
        For j As Integer = 0 To k - 1
            indexs(j) = j
            If firstPair.Equals("") Then
                firstPair = firstPair & items(j)
            Else
                firstPair = firstPair & "," & items(j)
            End If
            'firstSubList.Add(items(j))
        Next
        'añade la primera sub lista a la lista padre.
        finalPairList.Add(firstPair)
        ' añade los ultimos
        While indexs(0) <> n - k AndAlso finalPairList.Count < 2147483647
            If indexs(i) < i + (n - k) Then
                indexs(i) += 1
                Dim subPair As String = ""
                For Each j As Integer In indexs
                    'añade objetos a la sublista
                    If subPair.Equals("") Then
                        subPair = subPair & items(j)
                    Else
                        subPair = subPair & "," & items(j)
                    End If
                    'subList.Add(items(j))
                Next
                ' añade la sublista a la lista padre
                finalPairList.Add(subPair)
            Else
                Do
                    i -= 1
                Loop While indexs(i) = i + (n - k)
                indexs(i) += 1
                For j As Integer = i + 1 To k - 1
                    indexs(j) = indexs(j - 1) + 1
                Next
                Dim subPairList As String = ""
                For Each j As Integer In indexs
                    'añade los objetos a la sublista
                    If subPairList.Equals("") Then
                        subPairList = subPairList & items(j)
                    Else
                        subPairList = subPairList & "," & items(j)
                    End If
                    'subList.Add(items(j))
                Next
                'añade la sublista a la lista padre
                finalPairList.Add(subPairList)
                i = k - 1
            End If
        End While
        Return finalPairList
    End Function

    ''' <summary>
    ''' Calcula el numero de combinaciones unicas
    ''' </summary>
    ''' <param name="n">tamaño total de objetos</param>
    ''' <param name="k">tamaño del subconjunto</param>
    Public Function Count(ByVal n As ULong, ByVal k As ULong) As ULong
        If n < k Then Return 0
        If n = k Then Return 1
        Dim delta, iMax As ULong
        If (k < n - k) Then
            delta = n - k
            iMax = k
        Else
            delta = k
            iMax = n - k
        End If
        Dim ans As ULong = CULng(delta + 1)
        For i As ULong = 2 To iMax
            ans = CULng((ans * (delta + i)) / i)
        Next
        Return ans
    End Function

    ''' <summary>
    ''' Ordena los elementos en una lusta
    ''' </summary>
    ''' <param name="combLists">lista de combinaciones para ordenar</param>
    Public Overloads Sub Sort(ByVal combLists As List(Of List(Of Object)))
        If combLists.Count = 1 Then
            combLists(0).Sort()
        ElseIf combLists.Count > 1 Then
            combLists.Sort(New Combination)
        End If
    End Sub

    ''' <summary>
    ''' compara dos listas específicas
    ''' </summary>
    ''' <param name="x">primera lista</param>
    ''' <param name="y">segunda lista</param>
    Protected Overridable Function Compare(ByVal x As List(Of Object), ByVal y As List(Of Object)) As Integer _
    Implements System.Collections.Generic.IComparer(Of List(Of Object)).Compare
        x.Sort()
        y.Sort()
        Dim t As Integer
        'Numeric type compare
        If IsNumeric(x(0)) Then
            For i As Integer = 0 To x.Count - 1
                t = Val(x(i)).CompareTo(Val(y(i)))
                If t <> 0 Then
                    Return t
                End If
            Next i
            Return 0
        End If
        'String type compare
        If x(0).GetType Is GetType(String) Then
            For i As Integer = 0 To x.Count - 1
                t = CStr(x(i)).CompareTo(y(i))
                If t <> 0 Then
                    Return t
                End If
            Next i
            Return 0
        End If
        'Date type compare
        If x(0).GetType Is GetType(Date) Then
            For i As Integer = 0 To x.Count - 1
                t = CDate(x(i)).CompareTo(y(i))
                If t <> 0 Then
                    Return t
                End If
            Next i
            Return 0
        End If
    End Function

#End Region

End Class
