var tableProv = $('#dataTableProv').DataTable({
    "language": {
        "lengthMenu": "Mostrar _MENU_ ",
        "zeroRecords": "No hay Registros",
        "info": "Pagina _PAGE_ de _PAGES_",
        "infoEmpty": "No records available",
        "infoFiltered": "(filtered from _MAX_ total records)"
    },
    "lengthMenu": [[25, 50, -1], [25, 50, "All"]]
});


var idproveedor = 0;

$(function () {
    var valorDepartamento = 0;
    var valorMunicipio = 0;

    mostrarProveedores();

    $('#selectDepartamento').on('change', function () {
        valorDepartamento = $(this).val();
        console.log("dep " + valorDepartamento);

        $('#selectMunicipio').html("<option selected>Seleccionar Municipio</option>");
        llemarComboMunicipio(valorDepartamento);
    });

    $('#selectMunicipio').on('change', function () {
        valorMunicipio = $(this).val();
        console.log("Mun " + valorMunicipio);
    });

    $('#btnInsertarProveedor').click(function (even) {
        insertarProveedor(valorMunicipio);

        

        $("tbody").html("");
        mostrarProveedores();
    })


});

function mostrarProveedores() {
    var count = 1;

    $.ajax({
        type: "POST",
        url: "../Proveedores.asmx/obtenerProveedores",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            console.log("cargando");
        },
        success: function (response) {
            console.log(response);
            console.log(response.d);
            $.each(response.d, function (i, item) {

                tableProv.row.add(
                    [
                        count,
                        item.Nombre,
                        item.Nit,
                        item.Telefono,
                        item.Direccion,
                        item.Email,
                        item.Municipio,
                        "<a class='btn btn-outline-primary btnEditar' value=" + item.IdProveedor + " role='button'><i class='material-icons'>edit</i></a>" +
                        "<button type='button' class='btn btn-outline-primary'value=" + item.IdProveedor + "><i class='material-icons'>delete</i></button>" +
                                "<button type='button' class='btn btn-outline-primary'><i class='material-icons'>pageview</i></button></td>"]).draw(false);
                count++;
            });



            $(".btnEditar").click(function (event) {
                llemarComboDepartamento();
                banderaBtnGuardar = 1;
                idcliente = $(this).val();
                console.log(banderaBtnGuardar);
                console.log("idcliente: " + idcliente);
                $('#addClienteModal').modal('show');

                var id = { "idcliente": idcliente }
                $.ajax({
                    type: "POST",
                    url: "../ObtenerCliente.asmx/GetCustomer",
                    data: JSON.stringify(id),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        $.each(response.d, function (i, item) {
                            $('#primerNombre').val(item.Pnombre),
                           $('#segundoNombre').val(item.Snombre),
                           $('#primerApellido').val(item.Papellido),
                           $('#segundoApellido').val(item.Sapellido),
                           $('#dpi').val(item.Dpi),
                           $('#nit').val(item.Nit),
                           $('#telefono').val(item.Telefono),
                           $('#email').val(item.Email),
                           $('#direccion').val(item.Direccion)
                        });

                    },
                    error: function (r) {
                        alert(r.responseText + "Estamos teniendo Problemas");
                    },
                    failure: function (r) {
                        alert(r.responseText + "Estamos teniendo Problemas");
                    }
                });

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

function insertarProveedor(mun) {
    var parametros = {
        "nombre": $('#NombreProv').val(),
        "nit": $('#NitProv').val(),
        "email": $('#EmailProv').val(),
        "telefono": $('#TelefonoProv').val(),
        "direccion": $('#DireccionProv').val(),
        "municipio": mun
    };
    console.log(parametros);
    $.ajax({
        type: "POST",
        url: "../Proveedores.asmx/insertarProveedor",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("datos guardados");
            $('#addProveedorModal').modal('hide')
            myAlertBottom();

        },
        error: function (r) {
            alert(r.responseText + "Estamos teniendo Problemas");
        },
        failure: function (r) {
            alert(r.responseText + "Estamos teniendo Problemas");
        }
    });

}

function llemarComboDepartamento() {
    $('#selectDepartamento').html("<option selected>Seleccionar Departamento</option>");

    $.ajax({
        type: "POST",
        url: "../ObtenerLocalidad.asmx/GetDepartament",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            $.each(response.d, function (i, item) {

                $('#selectDepartamento').append(
                " <option value=" + item.IdDepartamento + ">" + item.Nombre + "</option>"
                );
            });

        },
        error: function (r) {
            alert(r.responseText + "Estamos teniendo Problemas");
        },
        failure: function (r) {
            alert(r.responseText + "Estamos teniendo Problemas");
        }
    });
}

function llemarComboMunicipio(i) {
    var iddepartamento = { "id": i }
    $.ajax({
        type: "POST",
        url: "../ObtenerLocalidad.asmx/GetMunicipio",
        data: JSON.stringify(iddepartamento),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            $.each(response.d, function (i, item) {
                $('#selectMunicipio').append(
                " <option value=" + item.IdMunicipio + ">" + item.Nombre + "</option>"
                );
            });

        },
        error: function (r) {
            alert(r.responseText + "estamos teniendo Problemas");
        },
        failure: function (r) {
            alert(r.responseText + "Estamos teniendo Problemas");
        }
    });
}



function myAlertBottom() {
    $(".myAlert-bottom").show();
    setTimeout(function () {
        $(".myAlert-bottom").hide();
    }, 2000);
}