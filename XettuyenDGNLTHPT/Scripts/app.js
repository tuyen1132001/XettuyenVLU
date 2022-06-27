//var baseService = "/Service";
//var tinhtpUrl = baseService + "/ServiceController/GetAllTinhTPs"
//var tp_qh_pxUrl = baseService + "/ServiceController/GetAllTP_QH_PXes"
//$(document).ready(function () {
//    _getTinhTPs();
//    $("#ddHoKhauTinhTP").on('chang', function () {
//        var id = $(this).val();
//        if (id != undefined && id != '') {}
//    })
//});
//function _getTinhTPs() {
//    $.get(tinhtpUrl, function (data) {
//        if (data != null && data != undefined && data.length) {
//            var html = '';
//            html += '<option value="">--Không chọn</option>';
//            $.each(data, function (key, item) {
//                html += '<option value=' + item.MA_TINHTP  '</option>';
//            })
//            $("#ddlHoKhauTinhTP").html(html);
//        }
//    })
//}