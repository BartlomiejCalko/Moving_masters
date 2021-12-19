var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/orders/GetAllOrders",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "referanceNumber", "width": "10%" },
            { "data": "customer.firstName", "width": "10%" },
            { "data": "customer.lastName", "width": "10%" },
            { "data": "customer.phone", "width": "10%" },
            { "data": "clientNewAddress", "width": "10%" },
            { "data": "choosenService", "width": "5%" },
            { "data": "description", "width": "10%" },
            { "data": "orderDate", "width": "10%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/orders/Upsert/${data}" class='btn btn-success text-white' 
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/orders/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete customer?",
        text: "You will not be able to restore data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}



