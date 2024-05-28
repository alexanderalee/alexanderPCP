Public Class Form1

    ' Product Prices
    Private Const XboxPrice As Decimal = 9999D
    Private Const PlayStationPrice As Decimal = 12999D
    Private Const HeadsetPrice As Decimal = 349D
    Private Const ControllerPrice As Decimal = 450D
    Private Const MonitorPrice As Decimal = 1999D

    ' VAT Rate
    Private Const VatRate As Decimal = 0.15D

    ' Discount Rates
    Private Const DiscountRate1 As Decimal = 0.2D
    Private Const DiscountRate2 As Decimal = 0.25D
    Private Const DiscountRate3 As Decimal = 0.3D

    Private Sub btnShowPromotionalOffer_Click(sender As Object, e As EventArgs) Handles btnShowPromotionalOffer.Click

        ' Validate user selection
        If Not radXbox.Checked And Not radPlayStation.Checked Then
            MessageBox.Show("Please select a product from Group A.")
            Return
        End If

        ' Calculate amounts
        Dim totalAmount As Decimal = CalculateTotalAmount()
        Dim discountAmount As Decimal = CalculateDiscountAmount()
        Dim vatAmount As Decimal = CalculateVatAmount(discountAmount)
        Dim finalAmount As Decimal = CalculateFinalAmount(totalAmount, discountAmount, vatAmount)

        ' Generate Quote
        GenerateQuote(totalAmount, discountAmount, vatAmount, finalAmount)
    End Sub

    Private Function CalculateTotalAmount() As Decimal

        Dim groupAPrice As Decimal
        If radXbox.Checked Then
            groupAPrice = XboxPrice
        ElseIf radPlayStation.Checked Then
            groupAPrice = PlayStationPrice
        End If

        Dim groupBTotal As Decimal = 0
        If chkHeadset.Checked Then
            groupBTotal += HeadsetPrice
        End If
        If chkController.Checked Then
            groupBTotal += ControllerPrice
        End If
        If chkMonitor.Checked Then
            groupBTotal += MonitorPrice
        End If

        Return groupAPrice + groupBTotal
    End Function

    Private Function CalculateDiscountAmount() As Decimal
        Dim totalGroupB As Decimal = 0
        Dim totalItemsB As Integer = 0

        If chkHeadset.Checked Then
            totalGroupB += HeadsetPrice
            totalItemsB += 1
        End If
        If chkController.Checked Then
            totalGroupB += ControllerPrice
            totalItemsB += 1
        End If
        If chkMonitor.Checked Then
            totalGroupB += MonitorPrice
            totalItemsB += 1
        End If

        ' Determine discount rate based on number of items in Group B
        Dim discountRate As Decimal = 0D
        Select Case totalItemsB
            Case 1
                discountRate = DiscountRate1

            Case 2
                discountRate = DiscountRate2

            Case Is >= 3
                discountRate = DiscountRate3

        End Select

        Return totalGroupB * discountRate

    End Function

    Private Function CalculateVatAmount(discountAmount As Decimal) As Decimal
        Dim totalAmount As Decimal = CalculateTotalAmount()
        Dim taxableAmount As Decimal = totalAmount - discountAmount
        Return taxableAmount * VatRate
    End Function

    Private Function CalculateFinalAmount(totalAmount As Decimal, discountAmount As Decimal, vatAmount As Decimal) As Decimal
        Return totalAmount - discountAmount + vatAmount
    End Function

    Private Sub GenerateQuote(totalAmount As Decimal, discountAmount As Decimal, vatAmount As Decimal, finalAmount As Decimal)

        LstQuote.Items.Clear()

        LstQuote.Items.Add("Customer Details:")
        LstQuote.Items.Add($"Name: {txtName.Text}")
        LstQuote.Items.Add($"Surname: {txtSurname.Text}")
        LstQuote.Items.Add($"Cellphone: {txtCellphone.Text}")
        LstQuote.Items.Add($"Email: {txtEmail.Text}")
        LstQuote.Items.Add("")
        LstQuote.Items.Add("Promotional Offer Details:")
        LstQuote.Items.Add($"Total Amount: R{totalAmount:0.00}")
        LstQuote.Items.Add($"Discount Amount: R{discountAmount:0.00}")
        LstQuote.Items.Add($"VAT Amount: R{vatAmount:0.00}")
        LstQuote.Items.Add($"Final Amount Payable: R{finalAmount:0.00}")
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        ' Clear all selections and inputs
        radXbox.Checked = False
        radPlayStation.Checked = False
        chkHeadset.Checked = False
        chkController.Checked = False
        chkMonitor.Checked = False

        txtName.Clear()
        txtSurname.Clear()
        txtCellphone.Clear()
        txtEmail.Clear()
        LstQuote.Items.Clear()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        ' Exit the application
        Me.Close()

    End Sub

End Class







