'////////////////////////////////////////////////////////////////////////////////
'	Events.cs
'	Managed Wiimote Library
'	Written by Brian Peek (http://www.brianpeek.com/)
'	for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
'	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
'  and http://www.codeplex.com/WiimoteLib
'	for more information
'////////////////////////////////////////////////////////////////////////////////


Imports Microsoft.VisualBasic
Imports System

Namespace WiimoteLib
	''' <summary>
	''' Argument sent through the WiimoteExtensionChangedEvent
	''' </summary>
	Public Class WiimoteExtensionChangedEventArgs
		Inherits EventArgs
		''' <summary>
		''' The extenstion type inserted or removed
		''' </summary>
		Public ExtensionType As ExtensionType
		''' <summary>
		''' Whether the extension was inserted or removed
		''' </summary>
		Public Inserted As Boolean

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="type">The extension type inserted or removed</param>
		''' <param name="inserted">Whether the extension was inserted or removed</param>
		Public Sub New(ByVal type As ExtensionType, ByVal inserted As Boolean)
			ExtensionType = type
			Me.Inserted = inserted
		End Sub
	End Class

	''' <summary>
	''' Argument sent through the WiimoteChangedEvent
	''' </summary>
	Public Class WiimoteChangedEventArgs
		Inherits EventArgs
		''' <summary>
		''' The current state of the Wiimote and extension controllers
		''' </summary>
		Public WiimoteState As WiimoteState

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="ws">Wiimote state</param>
		Public Sub New(ByVal ws As WiimoteState)
			WiimoteState = ws
		End Sub
	End Class
End Namespace
