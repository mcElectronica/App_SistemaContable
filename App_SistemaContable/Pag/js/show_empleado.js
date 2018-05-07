

        $(function () {
            var rsp;
        
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
                        console.log("esto es un cambio:"+item.Idcliente);
                            rsp = "<tr>"+
                                      "<td>"+item.Pnombre +" "+item.Snombre +"</td>"+
                                      "<td>"+item.Papellido +" "+item.Sapellido+"</td>"+
                                      "<td>"+item.Dpi+"</td>"+
                                      "<td>"+item.Telefono+"</td>"+
                                      "<td>"+item.Nit+"</td>"+
                                      "<td>"+item.Direccion +" "+item.Municipio+"</td>"+
                                      "<td><button type='button' class='btn btn-outline-primary'><i class='material-icons'>edit</i></button>"+
                                        "<button type='button' class='btn btn-outline-primary'><i class='material-icons'>delete</i></button>"+
                                        "<button type='button' class='btn btn-outline-primary'><i class='material-icons'>pageview</i></button></td>"+
                                    "</tr>";
                            
                            //agregamos al div contenido        
                            $('#cuerpo').append(rsp);
                       
                    });
                    
                    
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
            });
            return false;
      
    });