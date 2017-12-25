
let host = "lap080178";
let port = "45455";

$(document).ready(function () {
    let xmlhttpDevList, tableDevice, row, cellName;
    xmlhttpDevList = new XMLHttpRequest();
    xmlhttpDevList.onload = function (e) {
        if (xmlhttpDevList.readyState === 4) {
            if (xmlhttpDevList.status === 200) {
                let json = JSON.parse(xmlhttpDevList.responseText);

                for (let node in json) {
                    tableDevice = document.getElementById("deviceTable");
                    row = tableDevice.insertRow(0);

                    cellName = row.insertCell(0);
                    cellName.innerHTML = json[node].Id;
                    cellName.className = "deviceName";
                    row.insertCell(1).innerHTML = json[node].HostIp;
                    row.insertCell(2).innerHTML = json[node].DataSource;
                    row.insertCell(3).innerHTML = json[node].Group;
                    row.insertCell(4).innerHTML = json[node].Protocol;
                    row.insertCell(5).innerHTML = '<button class="btn btn-primary" onclick="openUpdateForm(this)"><span class="glyphicon glyphicon-pencil"></span></button>';
                    row.insertCell(6).innerHTML = '<button class="btn btn-primary" onclick="openDeleteForm(this)"><span class="glyphicon glyphicon-trash"></span></button>';
                } 
                $('#list').DataTable({
                    "aoColumnDefs": [{ "bSortable": false, "aTargets": [5, 6] }]
                });

            } else if (xmlhttpDevList.status === 400) {
                document.getElementById("errornew").innerHTML = xhttp.responseText;
            } else if (xmlhttpDevList.status === 404) {
                document.getElementById("errornew").innerHTML = "Keine Antwort von MessdatenServer!";
            }
        }
    };
    xmlhttpDevList.ontimeout = function () {
        document.getElementById("errornew").innerHTML = "Timeout, die Device-Liste kann nicht vom MessdatenServer geladen werden!";
    };
    xmlhttpDevList.open("GET", "../messdatenServer/list" + '?_=' + new Date().getTime(), true);
    xmlhttpDevList.timeout = 6000;
    xmlhttpDevList.send();
});


function formatItem(item) {
    return item.Name + ': $' + item.Price;
}

function find() {
    var id = $('#prodId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#product').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error: ' + err);
        });
}

//function toggleView() {
//    $("#list tbody").toggle("fast");
//    $("#id_form").toggle("fast");
//}


function openUpdateForm(anchor) {
    let cell = anchor.parentElement;
    let listRow = cell.parentElement;
    let name = listRow.cells[0].innerHTML;

   // window.open("http://" + host + ":" + port + "/View/update.html?Id=" + name + '&_=' + new Date().getTime(), '_self');
    window.open("/View/update.html?Id=" + name + '&_=' + new Date().getTime(), '_self');
}

function openDeleteForm(anchor) {
    let cell = anchor.parentElement;
    let listRow = cell.parentElement;
    let name = listRow.cells[0].innerHTML;

   // window.open("http://" + host + ":" + port + "/View/delete.html?Id=" + name + '&_=' + new Date().getTime(), '_self');
    window.open("/View/delete.html?Id=" + name + '&_=' + new Date().getTime(), '_self');
}

function openNewForm() {
    
   // window.open("http://" + host + ":" + port + "/View/new.html?_=" + new Date().getTime(), '_self');
    window.open("/View/new.html?_=" + new Date().getTime(), '_self');
}


