

Imports System.Data
Imports System.Data.SqlClient
Partial Class Spring_2022_Assignments_BasketballProject
    Inherits System.Web.UI.Page

#Region "declare"

    'declaring global datatables, data adapters, command builders, and connection strings
    Public Shared Con As New SqlConnection("Data Source=cb-ot-devst06.ad.wsu.edu;Initial Catalog = MF21jack.balster; Persist Security Info=True;User ID =jack.balster; Password=887b843d")

    Public Shared gdtPlayers As New DataTable
    Public Shared gdaPlayers As New SqlDataAdapter("SELECT * FROM dbo.PlayerInfo", Con)
    Public Shared cbPlayers As New SqlCommandBuilder(gdaPlayers)

    Public Shared gdtOnePlayer As New DataTable
    Public Shared gdaOnePlayer As New SqlDataAdapter("SELECT * FROM dbo.PlayerInfo WHERE PlayerID = @p1", Con)
    Public Shared cbUpdatePlayer As New SqlCommandBuilder(gdaOnePlayer)

    Public Shared gdtOneGame As New DataTable
    Public Shared gdaOneGame As New SqlDataAdapter("SELECT * FROM dbo.GameInfo WHERE GameDate = @p1", Con)
    Public Shared cbUpdateGame As New SqlCommandBuilder(gdaOneGame)

    Public Shared gdtCoaches As New DataTable
    Public Shared gdaCoaches As New SqlDataAdapter("SELECT * FROM dbo.CoachInfo", Con)
    Public Shared cbCoaches As New SqlCommandBuilder(gdaCoaches)

    Public Shared gdtOneCoach As New DataTable
    Public Shared gdaOneCoach As New SqlDataAdapter("SELECT * FROM dbo.CoachInfo WHERE CoachID = @p1", Con)
    Public Shared cbUpdateCoach As New SqlCommandBuilder(gdaOneCoach)

    Public Shared gdtArchivesTable As New DataTable
    Public Shared gdtCoachArchivesTable As New DataTable




#End Region

#Region "Links"
    'Setting link buttons to each view
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        'setting the link button to the appropiate view
        MultiView1.ActiveViewIndex = 0
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        'setting the link button to the appropiate view
        MultiView1.ActiveViewIndex = 1
    End Sub
    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        'setting the link button to the appropiate view
        MultiView1.ActiveViewIndex = 2
    End Sub

#End Region

    Private Sub Spring_2022_Assignments_BasketballProject_Init(sender As Object, e As EventArgs) Handles Me.Init

        'this line of code copies the SQL Server table schema into the array for each different table and arrays
        gdaPlayers.FillSchema(gdtPlayers, SchemaType.Mapped)
        gdaCoaches.FillSchema(gdtCoaches, SchemaType.Mapped)

    End Sub

#Region "Player Info"
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'clear gridview so data can be current and up to date
        gvPlayer.DataSource = Nothing
        gvPlayer.DataBind()


        Dim dr As DataRow = gdtPlayers.NewRow
        'make rows in the data table
        dr("PlayerName") = txtPlayerName.Text
        dr("Position") = txtPosition.Text
        dr("Phone") = txtPhone.Text
        dr("Height") = txtHeight.Text
        dr("Weight") = txtWeight.Text
        dr("Notes") = txtNotes.Text
        dr("ContractStart") = txtContractStart.Text
        dr("ContractEnd") = txtContractEnd.Text
        dr("ContractAmount") = txtContractAmount.Text
        dr("NumberOfGames") = 0


        'add the rows to the array
        gdtPlayers.Rows.Add(dr)

        'save data into the SQL database
        Try
            gdaPlayers.Update(gdtPlayers)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'refresh the gridview and update info
        Call GetAllPlayers()
        Call Update1Player()
        Call LoadDDL()

    End Sub

    Private Sub GetAllPlayers()

        'clear out old data in the array/refresh the gridview
        gvPlayer.DataSource = Nothing
        gvPlayer.DataBind()

        'if there are rows, clear them out
        If gdtPlayers.Rows.Count > 0 Then
            gdtPlayers.Rows.Clear()
        End If

        'get the data and display it in the gridview
        Try
            gdaPlayers.Fill(gdtPlayers)
            gvPlayer.DataSource = gdtPlayers
            gvPlayer.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Update1Player()

        'using SQL command to update the data in the table
        Dim cmdUpdatePlayer As New SqlCommand("UPDATE dbo.PlayerInfo SET ContractStart = @p1 WHERE ContractEnd = @p2", Con)


        With cmdUpdatePlayer.Parameters
            .Clear()
            .AddWithValue("@p1", DateTime.Parse(txtContractStart.Text))
            .AddWithValue("@p2", DateTime.Parse(txtContractEnd.Text))

        End With

        'manually open connection
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmdUpdatePlayer.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Con.Close()
        End Try

    End Sub
#Region "Retrieving data For 1 player"
    Protected Sub ddlPlayerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPlayerName.SelectedIndexChanged

        'if no player was selected then stop
        If ddlPlayerName.SelectedIndex <= 0 Then Exit Sub

        'clear out the rows from the global data table if there were any before
        If gdtOnePlayer.Rows.Count > 0 Then gdtOnePlayer.Rows.Clear()

        'retrieve one row of data
        With gdaOnePlayer.SelectCommand.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        Try
            gdaOnePlayer.Fill(gdtOnePlayer)
            gvPlayer.DataSource = gdtOnePlayer
            gvPlayer.DataBind()

            'take the values in the columns and assign them to textboxes to be displayed
            With gdtOnePlayer.Rows(0)
                txtPlayerName.Text = .Item("PlayerName")
                txtPosition.Text = .Item("Position")
                txtPhone.Text = .Item("Phone")
                txtHeight.Text = .Item("Height")
                txtWeight.Text = .Item("Weight")
                txtNotes.Text = .Item("Notes")
                txtContractStart.Text = .Item("ContractStart")
                txtContractEnd.Text = .Item("ContractEnd")
                txtContractAmount.Text = .Item("ContractAmount")

            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        'Display image based on player name selection (will have to input player picture manually when adding new players)
        Select Case ddlPlayerName.SelectedItem.Text
            Case "Lebron James"
                ddlPlayerName.SelectedItem.Text = "Lebron James"
                Image2.ImageUrl = "Lebronimg.png"
            Case "Klay Thompson"
                ddlPlayerName.SelectedItem.Text = "Klay Thompson"
                Image2.ImageUrl = "Klayimg.png"
            Case "Steph Curry"
                ddlPlayerName.SelectedItem.Text = "Steph Curry"
                Image2.ImageUrl = "Stephimg.png"
            Case "Kevin Durant"
                ddlPlayerName.SelectedItem.Text = "Kevin Durant"
                Image2.ImageUrl = "Kevinimg.png"
            Case "Kyrie Irving"
                ddlPlayerName.SelectedItem.Text = "Kyrie Irving"
                Image2.ImageUrl = "Kyrieimg.png"
            Case "Devin Booker"
                ddlPlayerName.SelectedItem.Text = "Devin Booker"
                Image2.ImageUrl = "Devinimg.png"
            Case "Dwayne Wade"
                ddlPlayerName.SelectedItem.Text = "Dwanye Wade"
                Image2.ImageUrl = "Wadeimg.png"
            Case "Luka Doncic"
                ddlPlayerName.SelectedItem.Text = "Luka Doncic"
                Image2.ImageUrl = "Lukaimg.png"
            Case "Jayson Tatum"
                ddlPlayerName.SelectedItem.Text = "Jayson Tatum"
                Image2.ImageUrl = "Tatumimg.png"
            Case "Lonzo Ball"
                ddlPlayerName.SelectedItem.Text = "Lonzo Ball"
                Image2.ImageUrl = "Lonzoimg.png"
            Case "Chris Paul"
                ddlPlayerName.SelectedItem.Text = "Chris Paul"
                Image2.ImageUrl = "Chrisimg.png"

        End Select
    End Sub

#Region "View #1 - Pushing changes back to database with dataAdapter.Update"
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            'identifying database table to webpage textbox values
            With gdtOnePlayer.Rows(0)
                .Item("PlayerName") = txtPlayerName.Text
                .Item("Position") = txtPosition.Text
                .Item("Phone") = txtPhone.Text
                .Item("Height") = txtHeight.Text
                .Item("Weight") = txtWeight.Text
                .Item("Notes") = txtNotes.Text
                .Item("ContractStart") = txtContractStart.Text
                .Item("ContractEnd") = txtContractEnd.Text
                .Item("ContractAmount") = txtContractAmount.Text
            End With

            'update the SQL database table
            gdaOnePlayer.Update(gdtOnePlayer)

            'refresh data source 
            With gvPlayer
                .DataSource = Nothing
                .DataSource = gdtOnePlayer
                .DataBind()
            End With

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#End Region

#Region "Coaches Info"
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'clear gridview so data can be current/updated
        gvCoach.DataSource = Nothing
        gvCoach.DataBind()


        Dim dr As DataRow = gdtCoaches.NewRow
        'make rows for coaches global data table
        dr("CoachName") = txtCoachName.Text
        dr("Title") = txtCoachTitle.Text
        dr("Phone") = txtPhone0.Text
        dr("Notes") = txtNotes0.Text
        dr("ContractStart") = txtContractStart0.Text
        dr("ContractEnd") = txtContractEnd0.Text
        dr("ContractAmount") = txtContractAmount0.Text

        'add the rows to the table
        gdtCoaches.Rows.Add(dr)


        'save the row of data to the data table
        Try
            gdaCoaches.Update(gdtCoaches)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'refresh the gridview and update individual info
        Call GetAllCoaches()
        Call Update1Coach()
        Call LoadDDL()

    End Sub

    Private Sub GetAllCoaches()

        'clear old data in array
        gvCoach.DataSource = Nothing
        gvCoach.DataBind()

        'if there are rows in the data table, clear them out
        If gdtCoaches.Rows.Count > 0 Then
            gdtCoaches.Rows.Clear()
        End If

        'display the data in the gridview
        Try
            gdaCoaches.Fill(gdtCoaches)
            gvCoach.DataSource = gdtCoaches
            gvCoach.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Update1Coach()

        'update coach data 
        Dim cmdUpdateCoach As New SqlCommand("UPDATE dbo.CoachInfo SET ContractStart = @p1 WHERE ContractEnd = @p2", Con)


        With cmdUpdateCoach.Parameters
            .Clear()
            .AddWithValue("@p1", DateTime.Parse(txtContractStart0.Text))
            .AddWithValue("@p2", DateTime.Parse(txtContractEnd0.Text))

        End With

        'manually open connection for SQL command
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmdUpdateCoach.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
#Region "Retrieving data For 1 Coach"
    Protected Sub ddlCoachName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCoachName.SelectedIndexChanged

        'if no coach was selected then exit program
        If ddlCoachName.SelectedIndex <= 0 Then Exit Sub

        'clear out the global data table if there is any info in it already
        If gdtOneCoach.Rows.Count > 0 Then gdtOneCoach.Rows.Clear()

        'Retrieve one row of data
        With gdaOneCoach.SelectCommand.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        'Display the data

        Try
            gdaOneCoach.Fill(gdtOneCoach)
            gvCoach.DataSource = gdtOneCoach
            gvCoach.DataBind()

            'take the values in the columns and assign them to textboxes 
            With gdtOneCoach.Rows(0)
                txtCoachName.Text = .Item("CoachName")
                txtCoachTitle.Text = .Item("Title")
                txtPhone0.Text = .Item("Phone")
                txtNotes0.Text = .Item("Notes")
                txtContractStart0.Text = .Item("ContractStart")
                txtContractEnd0.Text = .Item("ContractEnd")
                txtContractAmount0.Text = .Item("ContractAmount")

            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        'display the coach's picture depending on the selected name, (will have to manually add new pictures and new coaches join)
        Select Case ddlCoachName.SelectedItem.Text
            Case "Steve Nash"
                ddlCoachName.SelectedItem.Text = "Steve Nash"
                Image3.ImageUrl = "Steveimg.jpeg"
            Case "Jason Kidd"
                ddlCoachName.SelectedItem.Text = "Jason Kidd"
                Image3.ImageUrl = "Jasonimg.jpg"
            Case "Pat Riley"
                ddlCoachName.SelectedItem.Text = "Pat Riley"
                Image3.ImageUrl = "Patimg.jpg"
            Case "Doc Rivers"
                ddlCoachName.SelectedItem.Text = "Doc Rivers"
                Image3.ImageUrl = "Docimg.jpg"
            Case "Gregg Poppovich"
                ddlCoachName.SelectedItem.Text = "Gregg Poppovich"
                Image3.ImageUrl = "Popimg.jpg"


        End Select
    End Sub

#Region "View #1 - Pushing changes back to database with dataAdapter.Update"
    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            With gdtOneCoach.Rows(0)
                .Item("CoachName") = txtCoachName.Text
                .Item("Title") = txtCoachTitle.Text
                .Item("Phone") = txtPhone0.Text
                .Item("Notes") = txtNotes0.Text
                .Item("ContractStart") = txtContractStart0.Text
                .Item("ContractEnd") = txtContractEnd0.Text
                .Item("ContractAmount") = txtContractAmount0.Text

            End With

            'update the SQL data table
            gdaOneCoach.Update(gdtOneCoach)

            'refresh data source
            With gvCoach
                .DataSource = Nothing
                .DataSource = gdtOneCoach
                .DataBind()
            End With

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#End Region


#Region "Declare v2"

    'declaring more global data adapters, command builders, and data tables
    Public Shared daTotals As New SqlDataAdapter("Select TOP 1 * FROM dbo.GameInfo ORDER BY StatID DESC", Con)
    Public Shared cbTotals As New SqlCommandBuilder(daTotals)

    Public Shared PlayersDataTable As New DataTable
    Public Shared GameInfoDataTable As New DataTable
#End Region

#Region "Load DDL"

    'load drop down list for various tabs/tables
    Private Sub LoadDDL()

        'data adapter for game table
        Dim GameAdapter As New SqlDataAdapter("SELECT GameDate, StatID FROM dbo.GameInfo", Con)
        Dim GameDDLTable As New DataTable

        Try
            GameAdapter.Fill(GameDDLTable)
            'show statID and use game date to capture info for the game info table
            With ddlStatNumber
                .DataSource = GameDDLTable
                .DataTextField = "StatID"
                .DataValueField = "GameDate"
                .DataBind()
                .Items.Insert(0, "Select a StatID")
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        'data adapter for player table
        Dim PlayerAdapter As New SqlDataAdapter("SELECT PlayerID, PlayerName FROM dbo.PlayerInfo ORDER BY PlayerName", Con)
        Dim PlayerDDLTable As New DataTable

        Try
            PlayerAdapter.Fill(PlayerDDLTable)
            'show PlayerName and use PlayerID to capture info for the Player Info table
            With ddlPlayers
                .DataSource = PlayerDDLTable
                .DataTextField = "PlayerName"
                .DataValueField = "PlayerID"
                .DataBind()
                .Items.Insert(0, "Select a Player")
            End With
            'show PlayerName and use PlayerID to capture info for the Player Info table (ddl for game page)
            With ddlPlayerName
                .DataSource = PlayerDDLTable
                .DataTextField = "PlayerName"
                .DataValueField = "PlayerID"
                .DataBind()
                .Items.Insert(0, "Select a Player")
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        'data adapter for coach table
        Dim CoachAdapter As New SqlDataAdapter("SELECT CoachID, CoachName FROM dbo.CoachInfo", Con)
        Dim CoachDDLTable As New DataTable

        Try
            CoachAdapter.Fill(CoachDDLTable)
            'show CoachrName and use CoachID to capture info for the Coach Info table
            With ddlCoachName
                .DataSource = CoachDDLTable
                .DataTextField = "CoachName"
                .DataValueField = "CoachID"
                .DataBind()
                .Items.Insert(0, "Select a Coach")
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub
    Protected Sub Page_init(sender As Object, e As System.EventArgs) Handles Me.Init

        Call LoadDDL()

    End Sub

#End Region

#Region "Insert data row"

    Protected Sub btnAddStats_Click(sender As Object, e As EventArgs) Handles btnAddStats.Click

        'Adding schema to table
        daTotals.FillSchema(GameInfoDataTable, SchemaType.Mapped)
        'clear table if data is already there
        If GameInfoDataTable.Rows.Count > 0 Then GameInfoDataTable.Rows.Clear()
        Dim dr As DataRow = GameInfoDataTable.NewRow


        'adding the datarows and defining them
        dr.Item("PlayerID") = ddlPlayers.SelectedValue
        dr.Item("PlayerName") = ddlPlayers.SelectedItem.Text
        dr.Item("GameDate") = DateTime.Parse(txtGameDate.Text)
        dr.Item("Points") = Convert.ToDecimal(txtPoints.Text)
        dr.Item("Assists") = Convert.ToDecimal(txtAssists.Text)
        dr.Item("Rebounds") = Convert.ToDecimal(txtRebounds.Text)
        dr.Item("Steals") = Convert.ToDecimal(txtSteals.Text)
        dr.Item("Blocks") = Convert.ToDecimal(txtBlocks.Text)
        dr.Item("Turnovers") = Convert.ToDecimal(txtTurnovers.Text)

        'update the data
        Try
            GameInfoDataTable.Rows.Add(dr)
            daTotals.Update(GameInfoDataTable)
            Call UpdatePlayerData()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

#Region "UpdateIndividualData"

    Protected Sub UpdatePlayerData()

        'define what will be updated

        Dim cmdUpdatePlayerInfo As New SqlCommand("UPDATE dbo.PlayerInfo Set NumberOfGames +=1, PlayerName = @p2 WHERE PlayerID = @p1", Con)

        'update data in the rows using parameters to define 
        With cmdUpdatePlayerInfo.Parameters
            .Clear()
            .AddWithValue("@p1", (ddlPlayers.SelectedValue))
            .AddWithValue("@p2", (ddlPlayers.SelectedItem.Text))
        End With

        'open connection
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmdUpdatePlayerInfo.ExecuteNonQuery()
            'retrieving and displaying data
            Call GetTotals()
            Call RefreshPlayerGV()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Con.Close()
        End Try

    End Sub




#End Region

#Region "Show data row just added"
    Protected Sub GetTotals()

        'clear out rows from the global data table
        If GameInfoDataTable.Rows.Count > 0 Then GameInfoDataTable.Rows.Clear()

        'Fill and display the gridview
        Try
            daTotals.Fill(GameInfoDataTable)
            gvTotals.DataSource = GameInfoDataTable
            gvTotals.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

#Region "Show totals"
    Protected Sub RefreshPlayerGV()

        Dim PlayerAdapter As New SqlDataAdapter("SELECT PlayerID, PlayerName, Points, Assists, Rebounds, Steals, Blocks, Turnovers FROM dbo.GameInfo ORDER BY PlayerID", Con)

        Try 'clear the datatable if there are any info remaining

            If PlayersDataTable.Rows.Count > 0 Then PlayersDataTable.Rows.Clear()
            'Fill and display the gridview
            PlayerAdapter.Fill(PlayersDataTable)
            gvPlayers.DataSource = PlayersDataTable
            gvPlayers.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

#Region "Images"
    Protected Sub ddlPlayers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPlayers.SelectedIndexChanged

        'display player image based on the name selection (for the game info view)
        Select Case ddlPlayers.SelectedItem.Text
            Case "Lebron James"
                ddlPlayers.SelectedItem.Text = "Lebron James"
                Image1.ImageUrl = "Lebronimg.png"
            Case "Klay Thompson"
                ddlPlayers.SelectedItem.Text = "Klay Thompson"
                Image1.ImageUrl = "Klayimg.png"
            Case "Steph Curry"
                ddlPlayers.SelectedItem.Text = "Steph Curry"
                Image1.ImageUrl = "Stephimg.png"
            Case "Kevin Durant"
                ddlPlayers.SelectedItem.Text = "Kevin Durant"
                Image1.ImageUrl = "Kevinimg.png"
            Case "Kyrie Irving"
                ddlPlayers.SelectedItem.Text = "Kyrie Irving"
                Image1.ImageUrl = "Kyrieimg.png"
            Case "Devin Booker"
                ddlPlayers.SelectedItem.Text = "Devin Booker"
                Image1.ImageUrl = "Devinimg.png"
            Case "Dwayne Wade"
                ddlPlayers.SelectedItem.Text = "Dwanye Wade"
                Image1.ImageUrl = "Wadeimg.png"
            Case "Luka Doncic"
                ddlPlayers.SelectedItem.Text = "Luka Doncic"
                Image1.ImageUrl = "Lukaimg.png"
            Case "Jayson Tatum"
                ddlPlayers.SelectedItem.Text = "Jayson Tatum"
                Image1.ImageUrl = "Tatumimg.png"
            Case "Lonzo Ball"
                ddlPlayers.SelectedItem.Text = "Lonzo Ball"
                Image1.ImageUrl = "Lonzoimg.png"
            Case "Chris Paul"
                ddlPlayers.SelectedItem.Text = "Chris Paul"
                Image1.ImageUrl = "Chrisimg.png"

        End Select
    End Sub

#End Region

#Region "Retrieving data for 1 previous game"
    Protected Sub ddlStatNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatNumber.SelectedIndexChanged

        'if no statID was selected then exit the program
        If ddlStatNumber.SelectedIndex <= 0 Then Exit Sub

        'clear any exisisting data
        If gdtOneGame.Rows.Count > 0 Then gdtOneGame.Rows.Clear()

        'Retrieve one row of data

        With gdaOneGame.SelectCommand.Parameters
            .Clear()
            .AddWithValue("@p1", ddlStatNumber.SelectedValue)
        End With

        'display the data in the gridview
        Try
            gdaOneGame.Fill(gdtOneGame)
            gvTotals.DataSource = gdtOneGame
            gvTotals.DataBind()

            gvPlayers.DataSource = Nothing
            gvPlayers.DataBind()

            'assign values in textboxes to columns
            With gdtOneGame.Rows(0)
                txtPlayerID1.Text = .Item("PlayerID")
                txtPlayerName1.Text = .Item("PlayerName")
                txtGameDate1.Text = .Item("GameDate")
                txtPoints1.Text = .Item("Points")
                txtAssists1.Text = .Item("Assists")
                txtRebounds1.Text = .Item("Rebounds")
                txtSteals1.Text = .Item("Steals")
                txtBlocks1.Text = .Item("Blocks")
                txtTurnovers1.Text = .Item("Turnovers")


            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

#Region "View #1 - Pushing stat changes back to database with dataAdapter.Update"
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Try
            'push changes made in textboxes back to database
            With gdtOneGame.Rows(0)
                .Item("PlayerID") = txtPlayerID1.Text
                .Item("PlayerName") = txtPlayerName1.Text
                .Item("GameDate") = txtGameDate1.Text
                .Item("Points") = txtPoints1.Text
                .Item("Assists") = txtAssists1.Text
                .Item("Rebounds") = txtRebounds1.Text
                .Item("Steals") = txtSteals1.Text
                .Item("Blocks") = txtBlocks1.Text
                .Item("Turnovers") = txtTurnovers1.Text

            End With
            Dim cmdUpdatePlayerInfo As New SqlCommand("UPDATE dbo.PlayerInfo Set PointsLG = @p2, AssistsLG = @p3, ReboundsLG = @p4, StealsLG = @p5, BlocksLG = @p6, TurnoversLG = @p7 WHERE PlayerID = @p1", Con)

            'Update data in the rows
            With cmdUpdatePlayerInfo.Parameters
                .Clear()
                .AddWithValue("@p1", (ddlPlayers.SelectedValue))
                .AddWithValue("@p2", (txtPoints1.Text))
                .AddWithValue("@p3", (txtAssists1.Text))
                .AddWithValue("@p4", (txtRebounds1.Text))
                .AddWithValue("@p5", (txtSteals1.Text))
                .AddWithValue("@p6", (txtBlocks1.Text))
                .AddWithValue("@p7", (txtTurnovers1.Text))


            End With
            'update SQL database table
            gdaOneGame.Update(gdtOneGame)

            'display the changes
            With gvTotals
                .DataSource = Nothing
                .DataSource = gdtOneGame
                .DataBind()
            End With

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

#Region "Clear"
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Procedure to clear out inputs on the game info page

        ddlPlayers.SelectedIndex = -1

        txtGameDate.Text = Nothing
        txtPlayerName.Text = Nothing
        txtPoints.Text = Nothing
        txtAssists.Text = Nothing
        txtRebounds.Text = Nothing
        txtSteals.Text = Nothing
        txtBlocks.Text = Nothing
        txtTurnovers.Text = Nothing
        ddlStatNumber.SelectedIndex = -1
        txtPlayerID1.Text = Nothing
        txtGameDate1.Text = Nothing
        txtPlayerName1.Text = Nothing
        txtPoints1.Text = Nothing
        txtAssists1.Text = Nothing
        txtRebounds1.Text = Nothing
        txtSteals1.Text = Nothing
        txtBlocks1.Text = Nothing
        txtTurnovers1.Text = Nothing
        Image1.ImageUrl = Nothing
        gvTotals.DataSource = Nothing
        gvTotals.DataBind()
        gvPlayers.DataSource = Nothing
        gvPlayers.DataBind()

    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        'clear out the values in the player info page
        ddlPlayerName.SelectedIndex = -1
        txtPlayerName.Text = Nothing
        txtPosition.Text = Nothing
        txtPhone.Text = Nothing
        txtHeight.Text = Nothing
        txtWeight.Text = Nothing
        txtNotes.Text = Nothing
        txtContractStart.Text = Nothing
        txtContractEnd.Text = Nothing
        txtContractAmount.Text = Nothing
        Image2.ImageUrl = Nothing
        gvPlayer.DataSource = Nothing
        gvPlayer.DataBind()

    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        'clear out the values in coach info page
        ddlCoachName.SelectedIndex = -1
        txtCoachName.Text = Nothing
        txtPhone0.Text = Nothing
        txtNotes0.Text = Nothing
        txtContractStart0.Text = Nothing
        txtContractEnd0.Text = Nothing
        txtContractAmount0.Text = Nothing
        txtCoachTitle.Text = Nothing
        Image3.ImageUrl = Nothing
        gvCoach.DataSource = Nothing
        gvCoach.DataBind()
    End Sub
#End Region

#Region "Delete Data"
    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        Dim numPlayer As Integer

        Dim cmdCountNumOnePlayer As New SqlCommand("SELECT COUNT(*) From dbo.PlayerInfo WHERE PlayerID = @p1", Con)

        Dim cmdSaveToArchive As New SqlCommand("INSERT INTO dbo.PlayerInfoArchives SELECT * FROM dbo.PlayerInfo WHERE PlayerID = @p1", Con)

        Dim DaShowArchives As New SqlDataAdapter("SELECT * FROM dbo.PlayerInfoArchives WHERE PlayerID = @p1", Con)

        Dim cmdDeleteNumBeforePlayer As New SqlCommand("DELETE From dbo.PlayerInfo WHERE PlayerID = @p1", Con)

        Dim cmdDeletePlayer As New SqlCommand("DELETE From dbo.PlayerInfo WHERE PlayerID = @p1", Con)

        With cmdSaveToArchive.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        With DaShowArchives.SelectCommand.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        With cmdCountNumOnePlayer.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        With cmdDeletePlayer.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        With cmdDeleteNumBeforePlayer.Parameters
            .Clear()
            .AddWithValue("@p1", ddlPlayerName.SelectedValue)
        End With

        Try
            If Con.State = ConnectionState.Closed Then Con.Open()

            numPlayer = cmdCountNumOnePlayer.ExecuteScalar

            If numPlayer >= 1 Then
                'save to archive
                cmdSaveToArchive.ExecuteNonQuery()
                'if there are prior rows clear them out then display current data
                If gdtArchivesTable.Rows.Count > 0 Then gdtArchivesTable.Rows.Clear()
                DaShowArchives.Fill(gdtArchivesTable)
                gvPlayer.DataSource = gdtArchivesTable
                gvPlayer.DataBind()

                'delete record
                cmdDeleteNumBeforePlayer.ExecuteNonQuery()
                cmdDeletePlayer.ExecuteNonQuery()

            Else

                'delete record
                If Con.State = ConnectionState.Closed Then Con.Open()
                cmdDeletePlayer.ExecuteNonQuery()
            End If

            'refresh drop down list
            LoadDDL()
            'resfresh player list in gridview 
            Call GetAllPlayers()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Con.Close()
        End Try

        'clear all inputs to highlight changes made
        ddlPlayerName.SelectedIndex = -1
        txtPlayerName.Text = Nothing
        txtPosition.Text = Nothing
        txtPhone.Text = Nothing
        txtHeight.Text = Nothing
        txtWeight.Text = Nothing
        txtNotes.Text = Nothing
        txtContractStart.Text = Nothing
        txtContractEnd.Text = Nothing
        txtContractAmount.Text = Nothing
        Image2.ImageUrl = Nothing
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        'refer to button 10 click (deleting a player) for explanations/comments 
        'deleting a coach is the exact same process as deleting a player


        Dim numCoach As Integer

        Dim cmdCountNumOneCoach As New SqlCommand("SELECT COUNT(*) From dbo.CoachInfo WHERE CoachID = @p1", Con)

        Dim cmdSaveToArchive As New SqlCommand("INSERT INTO dbo.CoachInfoArchive SELECT * FROM dbo.CoachInfo WHERE CoachID = @p1", Con)

        Dim DaShowArchives As New SqlDataAdapter("SELECT * FROM dbo.CoachInfoArchive WHERE CoachID = @p1", Con)

        Dim cmdDeleteNumBeforeCoach As New SqlCommand("DELETE From dbo.CoachInfo WHERE CoachID = @p1", Con)

        Dim cmdDeleteCoach As New SqlCommand("DELETE From dbo.CoachInfo WHERE CoachID = @p1", Con)

        With cmdSaveToArchive.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        With DaShowArchives.SelectCommand.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        With cmdCountNumOneCoach.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        With cmdDeleteCoach.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        With cmdDeleteNumBeforeCoach.Parameters
            .Clear()
            .AddWithValue("@p1", ddlCoachName.SelectedValue)
        End With

        Try
            If Con.State = ConnectionState.Closed Then Con.Open()

            numCoach = cmdCountNumOneCoach.ExecuteScalar

            If numCoach >= 1 Then

                cmdSaveToArchive.ExecuteNonQuery()

                If gdtCoachArchivesTable.Rows.Count > 0 Then gdtCoachArchivesTable.Rows.Clear()
                DaShowArchives.Fill(gdtArchivesTable)
                gvCoach.DataSource = gdtArchivesTable
                gvCoach.DataBind()

                cmdDeleteNumBeforeCoach.ExecuteNonQuery()
                cmdDeleteCoach.ExecuteNonQuery()

            Else

                If Con.State = ConnectionState.Closed Then Con.Open()
                cmdDeleteCoach.ExecuteNonQuery()
            End If

            LoadDDL()

            Call GetAllCoaches()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Con.Close()
        End Try

        ddlCoachName.SelectedIndex = -1
        txtCoachName.Text = Nothing
        txtCoachTitle.Text = Nothing
        txtPhone0.Text = Nothing
        txtNotes0.Text = Nothing
        txtContractStart0.Text = Nothing
        txtContractEnd0.Text = Nothing
        txtContractAmount0.Text = Nothing
        Image3.ImageUrl = Nothing
    End Sub
#End Region
End Class

