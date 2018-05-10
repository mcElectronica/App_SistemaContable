$(function () {
    var idpregunta;
    mostrarDatos();


      
});

function mostrarDatos() {
    var table = $('#dataTablee').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ ",
            "zeroRecords": "No hay Registros",
            "info": "Pagina _PAGE_ de _PAGES_",
            "infoEmpty": "No records available",
            "infoFiltered": "(filtered from _MAX_ total records)"
        }
    });

    $.ajax({
        type: "POST",
        url: "../ObtenerCliente.asmx/GetCustomerNames",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            console.log(response.d);
            $.each(response.d, function (i, item) {
                
                table.row.add(
                    [
                        item.Pnombre + " " + item.Snombre,
                        item.Papellido + " " + item.Sapellido,
                        item.Dpi,
                        item.Telefono,
                        item.Nit,
                        item.Direccion + " " + item.Municipio,
                        "<a class='btn btn-outline-primary btnEditar' href=" + item.Idcliente + " role='button'><i class='material-icons'>edit</i></a>" +
                        "<button type='button' class='btn btn-outline-primary'><i class='material-icons'>delete</i></button>" +
                                "<button type='button' class='btn btn-outline-primary'><i class='material-icons'>pageview</i></button></td>"]).draw(false);

            });

            $(".btnEditar").click(function (event) {
                event.preventDefault();
                var idpregunta = $(this).attr('href');
                console.log(idpregunta);
                
            });

        },
        error: function (r) {
            alert(r.responseText + "Noconectada");
        },
        failure: function (r) {
            alert(r.responseText + "Noconectada");
        }
    });

}

function insertarCliente() {
    var parametros = {
        "pnombre": $('#primerNombre').val(),
        "snombre": $('#segundoNombre').val(),
        "papellido": $('#primerApellido').val(),
        "sapellido": $('#segundoApellido').val(),
        "dpi": $('#dpi').val(),
        "nit": $('#nit').val(),
        "telefono": $('#telefono').val(),
        "email": $('#email').val(),
        "direccion": $('#direccion').val(),
        "municipio": $('#municipio').val()
    };
    console.log(parametros);
    $.ajax({
        type: "POST",
        url: "../InsertarCliente.asmx/addcustumers",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("datos guardados");
            $('#addClienteModal').modal('hide')
            
        },
        error: function (r) {
            alert(r.responseText + "bononmalo");
        },
        failure: function (r) {
            alert(r.responseText + "botonmalo");
        }
    });

}

function myAlertTop() {
    $(".myAlert-top").show();
    setTimeout(function () {
        $(".myAlert-top").hide();
    }, 2000);
}

function myAlertBottom() {
    $(".myAlert-bottom").show();
    setTimeout(function () {
        $(".myAlert-bottom").hide();
    }, 2000);
}