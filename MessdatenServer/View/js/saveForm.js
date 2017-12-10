﻿
function saveDevice() {

    if (!isNewDeviceFormValid()) {
        return false;
    }

    let xhttp = new XMLHttpRequest();
    xhttp.onload = function (e) {
        if (xhttp.readyState === 4) {
            if (xhttp.status === 200) {
                openListForm();
            } else if (xhttp.status === 400) {
                window.alert(xhttp.responseText);
            }
        }
    };
    xhttp.open("POST", "../messdatenServer/save", true);
    xhttp.setRequestHeader("Content-type", "application/json; charset=utf-8");
    
    let jsonStr = JSON.stringify({
        "Id": document.getElementById("name").value.trim(),
        "HostIp": document.getElementById("hostIp").value.trim(),
        "DataSource": document.getElementById("dataSource").value.trim(),
        "Group": document.getElementById("group").value.trim(),
        "Protocol": document.getElementById("protocol").value.trim()
    });
    xhttp.send(jsonStr);
}

function openListForm() {
    let url = "/View/index.html?_=" + new Date().getTime();

    window.open(url, '_self');
}

function isNewDeviceFormValid() {

    if ( (!document.getElementById("name").value)
       ||(!document.getElementById("dataSource").value)
       ||(!document.getElementById("protocol").value)
       )
    {
        window.alert("Eingabefelder für neuen Device nicht vollständig!\n\nBenötigte Felder:\n\n-Id\n-DataSource\n-Protocol");
        return false;
    }
    return true;
}