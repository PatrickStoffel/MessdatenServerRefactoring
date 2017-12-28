
let name = getUrlParameterByName('Id');

let xmlhttpDevList = new XMLHttpRequest();
xmlhttpDevList.onload = function (e) {
    if (xmlhttpDevList.readyState === 4) {
        if (xmlhttpDevList.status === 200) {
            let json = JSON.parse(xmlhttpDevList.responseText);

            document.getElementById("name").value = json.Id;
            document.getElementById("hostIp").value = json.HostIp;
            document.getElementById("dataSource").value = json.DataSource;
            document.getElementById("group").value = json.Group;
            document.getElementById("protocol").value = json.Protocol;

        } else if (xmlhttpDevList.status === 400) {

        }
    }
};
xmlhttpDevList.open("GET", "../messdatenServer/device/" + name + '?_=' + new Date().getTime(), true);
xmlhttpDevList.send();


function getUrlParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


function updateDevice() {

    if (!isUdatedDeviceFormValid()) {
        document.getElementById("errornew").innerHTML = "Eingabefelder für Device nicht vollständig! Benötigte Felder: Id, HostIp, DataSource und Protocol";
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
    xhttp.open("POST", "../messdatenServer/update", true);
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


function deleteDevice() {

    let xmlhttp = new XMLHttpRequest();
    let name = document.getElementById("name").value;
    xmlhttp.onload = function (e) {
        if (xmlhttp.readyState === 4) {
            if (xmlhttp.status === 200) {
                openListForm();
            } else if (xmlhttp.status === 400) {
                window.alert(xmlhttp.responseText);
            }
        }
    };
    xmlhttp.open("GET", "../messdatenServer/delete/" + name, true);
    xmlhttp.send();
}

function isUdatedDeviceFormValid() {

    if ((!document.getElementById("name").value)
        || (!document.getElementById("hostIp").value)
        || (!document.getElementById("dataSource").value)
        || (!document.getElementById("protocol").value)
    ) {
        return false;
    }
    return true;
}