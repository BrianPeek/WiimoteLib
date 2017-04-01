'////////////////////////////////////////////////////////////////////////////////
'	WiimoteCollection.cs
'	Managed Wiimote Library
'	Written by Brian Peek (http://www.brianpeek.com/)
'	for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'	for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System.Collections.ObjectModel

Namespace WiimoteLib
	''' <summary>
	''' Used to manage multiple Wiimotes
	''' </summary>
	Public Class WiimoteCollection
		Inherits Collection(Of Wiimote)
		''' <summary>
		''' Finds all Wiimotes connected to the system and adds them to the collection
		''' </summary>
		Public Sub FindAllWiimotes()
			Wiimote.FindWiimote(AddressOf WiimoteFound)
		End Sub

		Private Function WiimoteFound(ByVal devicePath As String) As Boolean
			Me.Add(New Wiimote(devicePath))
			Return True
		End Function
	End Class
End Namespace
