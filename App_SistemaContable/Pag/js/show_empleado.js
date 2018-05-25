var table = $('#dataTablee').DataTable({
    "language": {
        "lengthMenu": "Mostrar _MENU_ ",
        "zeroRecords": "No hay Registros",
        "info": "Pagina _PAGE_ de _PAGES_",
        "infoEmpty": "No records available",
        "infoFiltered": "(filtered from _MAX_ total records)"
    },
    "lengthMenu": [[25, 50, -1], [25, 50, "All"]]
});
var idcliente = 0;
var banderaBtnGuardar = 0;

$(function () {
    var valorDepartamento = 0;
    var valorMunicipio = 0;
    


    mostrarDatos();

    $('#btnaddcliente').click(function(){
        llemarComboDepartamento();
        $('#primerNombre').val(""),
           $('#segundoNombre').val(""),
           $('#primerApellido').val(""),
           $('#segundoApellido').val(""),
           $('#dpi').val(""),
           $('#nit').val(""),
           $('#telefono').val(""),
           $('#email').val(""),
           $('#direccion').val("");
    });

    $('#selectDepartamento').on('change', function () { 
        valorDepartamento = $(this).val();

        $('#selectMunicipio').html("<option selected>Seleccionar Municipio</option>");
        llemarComboMunicipio(valorDepartamento);
    });

    $('#selectMunicipio').on('change', function () {
        valorMunicipio = $(this).val();
        console.log("Mun " + valorMunicipio);
    });

    $("#btnInsertarCliente").click(function () {

        if (banderaBtnGuardar == 0) {
            
            insertarCliente(valorMunicipio);
        
            $('#primerNombre').val(""),
            $('#segundoNombre').val(""),
            $('#primerApellido').val(""),
            $('#segundoApellido').val(""),
            $('#dpi').val(""),
            $('#nit').val(""),
            $('#telefono').val(""),
            $('#email').val(""),
            $('#direccion').val("");
            
            window.setTimeout('location.reload()', 10); //Reloads after three seconds

        } else if (banderaBtnGuardar == 1) {
                banderaBtnGuardar = 0;
                actualizarcliente(valorMunicipio, idcliente)
                $('#primerNombre').val(""),
                $('#segundoNombre').val(""),
                $('#primerApellido').val(""),
                $('#segundoApellido').val(""),
                $('#dpi').val(""),
                $('#nit').val(""),
                $('#telefono').val(""),
                $('#email').val(""),
                $('#direccion').val("");
                
                window.setTimeout('location.reload()', 10); //Reloads after three seconds
          
            }
        
        
        
    });

});

function mostrarDatos() {
    var count = 1;

    $.ajax({
        type: "POST",
        url: "../ObtenerCliente.asmx/GetCustomerNames",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            console.log("cargando");
        },
        success: function (response) {
            console.log(response.d);
            $.each(response.d, function (i, item) {
                
                table.row.add(
                    [
                        count,
                        item.Pnombre + " " + item.Snombre,
                        item.Papellido + " " + item.Sapellido,
                        item.Dpi,
                        item.Telefono,
                        item.Nit,
                        item.Direccion + " " + item.Municipio,
                        "<button type='button' class='btn btn-outline-primary btnEditar' value=" + item.Idcliente + "><i class='material-icons'>edit</i></button>" +
                        "<button type='button' class='btn btn-outline-primary btnEliminar' value=" + item.Idcliente +"><i class='material-icons'>delete</i></button>" +
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

                var id = { "idcliente": idcliente}
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

            $(".btnEliminar").click(function (event) {
              
                idcliente = $(this).val();
                console.log("idcliente: " + idcliente);
                var iddlt = { "idcliente": idcliente }
                $.ajax({
                    type: "POST",
                    url: "../ObtenerCliente.asmx/DeleteCustomer",
                    data: JSON.stringify(iddlt),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log("dato eliminado");
                        window.setTimeout('location.reload()', 10); //Reloads after three seconds

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

function insertarCliente(mun) {
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
        "municipio": mun
    };
 
    $.ajax({
        type: "POST",
        url: "../InsertarCliente.asmx/addcustumers",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("datos guardados");
            $('#addClienteModal').modal('hide')
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
    var iddepartamento = {"id": i}
    $.ajax({
        type: "POST",
        url: "../ObtenerLocalidad.asmx/GetMunicipio",
        data: JSON.stringify(iddepartamento),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
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

function actualizarcliente(mun, idc) {
    var parametros = {
        "id": idc,
        "pnombre": $('#primerNombre').val(),
        "snombre": $('#segundoNombre').val(),
        "papellido": $('#primerApellido').val(),
        "sapellido": $('#segundoApellido').val(),
        "dpi": $('#dpi').val(),
        "nit": $('#nit').val(),
        "telefono": $('#telefono').val(),
        "email": $('#email').val(),
        "direccion": $('#direccion').val(),
        "municipio": mun
    };
    console.log("parametros actu");
    console.log(parametros);
    $.ajax({
        type: "POST",
        url: "../InsertarCliente.asmx/updatecustumers",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("datos guardados");
            $('#addClienteModal').modal('hide')
            myAlertBottom();

        },
        error: function (r) {
            
        },
        failure: function (r) {
            
        }
    });

}

