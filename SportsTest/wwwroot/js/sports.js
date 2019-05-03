var baseUrl = "https://localhost:44308/SportsApi/";
var mainUrl ="https://localhost:44308/"
$(document).ready(function () {
});
    $("#btnCreateTest").click(function () {
        var TestType = $("#selTest").val();
        var Date = $("#txtDate").val();
        if (TestType != "" && Date != "") {
            var data = { TestId: TestType, Date: Date }
            $.ajax({
                type: "POST",
                url: baseUrl + "CreateTest",
                data: data,
                success: function (data) {
                    if (data.resultCode == 1) {
                        loadAllTest();
                        $("#selTest").val(0);
                        $("#txtDate").val("");
                    }
                },

            });
        } else {
            alert("Please fill the blank field");
        }
        
    });
    //Add Athlete
    $("#btnAddAthelate").click(function () {
        var TestId = $("#hdTestId").val();
        var MemberId = $("#selMember").val();
        var Distance = $("#txtDistance").val();
        if (MemberId != "" && Distance != "") {
            var data = { MemberId: MemberId, TestDetailsId: TestId, Distance: Distance }
            $.ajax({
                type: "POST",
                url: baseUrl + "InsertAthlete",
                data: data,
                success: function (data) {
                    if (data.resultCode == 2) {
                        alert("Member Already added in this group");
                    } else if (data.resultCode == 1) {
                        loadAllMemberByTest(TestId);
                        $("#selMember").val(0);
                        $("#txtDistance").val(0);
                    }
                },

            });
        } else {
            alert("Please fill the blank field");
        }

    });

    function loadAllTest() {
        var html = "";
        $.ajax({
            type: "GET",
            url: baseUrl+"GetAllTest",
            success: function (data) {
                if (data.resultCode == 1) {
                    $.each(data.data, function (key, value) {

                        html = html + "<tr><td>" + value.date + "</td>";
                        html = html + "<td>" + value.noOfParticipants + "</td>";
                        html = html + "<td>" + value.testName + "</td>";
                        html = html + " <td><a href='/Tests/TestDeatils/" + value.testDeatilsId + "'>View Deatils</a></td></tr>";
                    });

                    $("#tblTestList").html(html);
                   

                }
            },

        });
    }

    function loadAllMemberByTest(id) {
        var html = "";
        var result = "";
        var slno = 1;
        $.ajax({
            type: "GET",
            url: baseUrl + "GetAllMemberByTest/"+id,
            success: function (data) {
                if (data.resultCode == 1) {
                    $.each(data.data, function (key, value) {

                        if (value.distance <= 1000) {
                            result = "Below Average";
                        } else if (value.distance > 1000 && value.distance <= 2000) {
                            result = "Average";
                        }
                        else if (value.distance > 2000 && value.distance <= 3500) {
                            result = "Good";
                        }
                         else if (value.distance > 3500) {
                            result = "Very Good";
                        }
                        
                        
                        html = html + "<tr><td>" + slno + "</td>";
                        html = html + "<td>" + value.memberName + "</td>";
                        html = html + "<td>" + value.distance + "</td>";
                        html = html + "<td>" + result + "</td>";
                        html = html + " <td><a href='#' data-toggle='modal' data-target='#exampleModalEdit' onclick='editMember(" + value.memberId + "," + value.distance + "," + value.memberTestId + "," + id + ")'>Edit</a> |<a href='#' onclick='deleteMember(" + value.memberTestId + "," + id + ")'>Delete</a></td></tr>";
                        slno = slno + 1;

                    });

                    $("#tblMemberList").html(html);


                }
            },

        });
    }





function deleteMember(id, testid) {

    var txt;
    var r = confirm("Are you sure want to delete?");
    if (r == true) {
        var html = "";
        $.ajax({
            type: "GET",
            url: baseUrl + "DeleteMemberFromTest/" + id,
            success: function (data) {
                if (data.resultCode == 1) {

                    loadAllMemberByTest(testid);

                }
            },

        });
    } else {
       
    }
    
}

function deleteTest(id) {
    var txt;
    var r = confirm("Are you sure want to delete?");
    if (r == true) {
        var html = "";
        $.ajax({
            type: "GET",
            url: baseUrl + "DeleteTest/" + id,
            success: function (data) {
                if (data.resultCode == 1) {

                    window.location.href = mainUrl

                }
            },

        });
    } else {

    }
}

function editMember(memberid, distance,membertestid) {
    $("#selEditMember").val(memberid);
    $("#txtEditDistance").val(distance);
    $("#hdMemberTestId").val(membertestid);
}

$("#btnEditAthelate").click(function () {

    var member = $("#selEditMember").val();
    var distance = $("#txtEditDistance").val();
    var testid = $("#hdTestId").val();
    var membertestid = $("#hdMemberTestId").val();
    var data = { MemberTestId: membertestid, MemberId: member, Distance: distance }
    $.ajax({
        type: "POST",
        url: baseUrl + "UpdateMember",
        data: data,
        success: function (data) {
            if (data.resultCode == 1) {
                $("#selEditMember").val("");
                $("#txtEditDistance").val("");
                $("#hdMemberTestId").val("");
                loadAllMemberByTest(testid);
            }
        },

    });
});
function updateMemberTest() {
   
}