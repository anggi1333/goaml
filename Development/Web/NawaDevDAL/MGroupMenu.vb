'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class MGroupMenu
    Public Property PK_MGroupMenu_ID As Integer
    Public Property GroupMenuName As String
    Public Property GroupMenuDesciption As String
    Public Property Active As Nullable(Of Boolean)
    Public Property CreatedBy As String
    Public Property LastUpdateBy As String
    Public Property ApprovedBy As String
    Public Property CreatedDate As Nullable(Of Date)
    Public Property LastUpdateDate As Nullable(Of Date)
    Public Property ApprovedDate As Nullable(Of Date)
    Public Property Alternateby As String
    Public Property DataMenu As String

    Public Overridable Property FileSecurities As ICollection(Of FileSecurity) = New HashSet(Of FileSecurity)

End Class
