<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PatientManager._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients Registration</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-GLhlTQ8iKt6U7u5NlHc5rKPx3eN0wPbT1D+2hjBIql6ZJ/Z4+bXpO5KU81iJ0" crossorigin="anonymous" />
</head>
<body>
    <div class="container mt-5">
        <h2>Patient Registration</h2>
        <form runat="server" id="patientForm" class="mt-4">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="uplPanel" UpdateMode="Conditional" runat="server" OnLoad="uplPanel_Load">
                <ContentTemplate>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                             <label for="lblID">ID</label>
                             <asp:Label ID="lblID" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group col-md-6">
                            <label for="txtFirstName">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group col-md-6">
                            <label for="txtLastName">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="txtPhone">Phone</label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group col-md-6">
                            <label for="txtEmail">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="ddlGender">Gender</label>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="txtNotes">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                        </div>
                    </div>
                    <button id="btnSave" type="button" class="btn btn-primary" onclick="SavePatient()">Save</button>
                    <button type="button" class="btn btn-primary" onclick="NewPatient()">New</button>
            
                    <asp:GridView ID="patientGridView" CssClass="container" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="patientGridView_RowDeleting">
                        <Columns>
                             <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <button type="button" class="btn btn-primary" 
                                        onclick="SelectPatient('<%# Eval("Id")%>', '<%# Eval("FirstName")%>', '<%# Eval("LastName")%>', '<%# Eval("Phone")%>', '<%# Eval("Email")%>', '<%# Eval("Gender")%>', '<%# Eval("Notes")%>')">
                                        Select</button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name"/>
                            <asp:BoundField DataField="Phone" HeaderText="Phone"/>
                            <asp:BoundField DataField="Email" HeaderText="E-mail"/>
                            <asp:BoundField DataField="Gender" HeaderText="Gender"/>
                            <asp:BoundField DataField="Notes" HeaderText="Notes" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" Visible ="false"/>
                            <asp:BoundField DataField="LastUpdatedDate" HeaderText="LastUpdatedDate" Visible ="false"/>
                            <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="" Text="Delete"/>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.7/dist/umd/popper.min.js" integrity="sha384-z9bTOOnJmTzHysv+ODty2tKTa0lM05/UajjCSeKR5gqNQKf1ibJfFSAUd8I6IYfF" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js" integrity="sha384-B4gt1jrGC7Jh4AgTPSdUtOBvfO8sh+WyICHO7F3UMPZ/8+UZTzRn3fg17FfZ1CZ" crossorigin="anonymous"></script>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
    <%--<!-- jQuery library -->
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.slim.min.js"></script>--%>
    <!-- Popper JS -->
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <!-- Latest compiled JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">

        function SelectPatient(id, firstName, lastName, phone, email, gender, notes) {
            document.getElementById('<%= lblID.ClientID %>').innerHTML = id;
            document.getElementById('<%= txtFirstName.ClientID %>').value = firstName;
            document.getElementById('<%= txtLastName.ClientID %>').value = lastName;
            document.getElementById('<%= txtPhone.ClientID %>').value = phone;
            document.getElementById('<%= txtEmail.ClientID %>').value = email;
            document.getElementById('<%= ddlGender.ClientID %>').value = gender;
            document.getElementById('<%= txtNotes.ClientID %>').value = notes;
        }

        function NewPatient() {
            document.getElementById('<%= lblID.ClientID %>').innerHTML = "";
            document.getElementById('<%= txtFirstName.ClientID %>').value = "";
            document.getElementById('<%= txtLastName.ClientID %>').value = "";
            document.getElementById('<%= txtPhone.ClientID %>').value = "";
            document.getElementById('<%= txtEmail.ClientID %>').value = "";
            document.getElementById('<%= ddlGender.ClientID %>').value = "";
            document.getElementById('<%= txtNotes.ClientID %>').value = "";
        }

        function SavePatient() {
            var validPatient = ValidateFields();

            if (!validPatient)
                return;
            
            jQuery.ajax({
                type: "POST",
                url: "Default.aspx/SavePatient",
                data: JSON.stringify(validPatient),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    alert('Patient information saved successfully.');
                    NewPatient();
                    __doPostBack('uplPanel', '');
                },
                error: function (error) {
                    alert('Error saving patient information.');
                    console.log(error);
                }
            });
        }

        function ValidateFields() {
            var id = document.getElementById('<%= lblID.ClientID %>').innerHTML;
            if (id == "") id = -1;
            var firstName = document.getElementById('<%= txtFirstName.ClientID %>').value;
            var lastName = document.getElementById('<%= txtLastName.ClientID %>').value;
            var phone = document.getElementById('<%= txtPhone.ClientID %>').value;
            var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var gender = document.getElementById('<%= ddlGender.ClientID %>').value;
            var notes = document.getElementById('<%= txtNotes.ClientID %>').value;

            if (!firstName || !lastName || !phone || !gender) {
                alert("Fields firstname, lastname, phone, gender are mandatory! Please inform it before save.")
                return;
            }

            var patient = {
                Id : id,
                FirstName: firstName,
                LastName: lastName,
                Phone: phone,
                Email: email,
                Gender: gender,
                Notes: notes,
                CreatedDate: new Date(),
                LastUpdatedDate: new Date(),
                IsDeleted: false
            };
            return patient;
        }
    </script>
</body>
</html>
