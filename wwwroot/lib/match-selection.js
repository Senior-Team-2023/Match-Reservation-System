$(document).ready(function () {
    loadTeamsDDL('HomeTeamId', 'AwayTeamId');
    loadLineMenDDL('LineMan2', 'LineMan1');
});

function loadTeamsDDL(Team_1_Id_DDL, Team_2_Id_DDL) {
    // Get the selected value of the first dropdown list
    var selectedValue = $("#" + Team_1_Id_DDL).val()
    var dropdown2 = $("#" + Team_2_Id_DDL);
    //console.log(dropdown2);
    var secondId = dropdown2.val();
    // Make an AJAX request to load the options for the second dropdown list
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Matches/GetAllTeamsExcept?teamId=" + selectedValue);
    xhr.onload = function () {
        // Parse the JSON response and add the options to the second dropdown list
        var options = JSON.parse(xhr.responseText);
        dropdown2.val("");
        dropdown2.empty();
        var isSelectedInSecondDDL = false;
        for (var i = 0; i < options.length; i++) {
            if (options[i].value == secondId) {
                isSelectedInSecondDDL = true;
            }
            var option = $("<option></option>");
            option.attr("value", options[i].value);
            option.text(options[i].text);
            dropdown2.append(option);
        }
        if (isSelectedInSecondDDL) {
            dropdown2.val(secondId);
        }
    };
    xhr.send();
}

function loadLineMenDDL(LineMan_1_Id_DDL, LineMan_2_Id_DDL) {
    // Get the selected value of the first dropdown list
    var selectedValue = $("#" + LineMan_1_Id_DDL).val()
    var dropdown2 = $("#" + LineMan_2_Id_DDL);
    //console.log(dropdown2);
    var secondId = dropdown2.val();
    // Make an AJAX request to load the options for the second dropdown list
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Matches/GetAllLineMenExcept?refereeId=" + selectedValue);
    xhr.onload = function () {
        // Parse the JSON response and add the options to the second dropdown list
        var options = JSON.parse(xhr.responseText);
        var dropdown2 = $("#" + LineMan_2_Id_DDL);
        dropdown2.val("");
        dropdown2.empty();
        var isSelectedInSecondDDL = false;
        for (var i = 0; i < options.length; i++) {
            if (options[i].value == secondId) {
                isSelectedInSecondDDL = true;
            }
            var option = $("<option></option>");
            option.attr("value", options[i].value);
            option.text(options[i].text);
            dropdown2.append(option);
        }
        if (isSelectedInSecondDDL) {
            dropdown2.val(secondId);
        }
    };
    xhr.send();
}
