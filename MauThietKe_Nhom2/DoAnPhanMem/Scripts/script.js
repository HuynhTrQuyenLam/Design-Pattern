function ShowImagePreview(imageUpload, previewImage){
    if (imageUpload.file && imageUpload.file[0]){
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src',e.target.result);
        }
        reader.readAsDataURL(imageUpload.file[0]);
    }
}
function validateDecimal(input) {
    // Loại bỏ bất kỳ ký tự không phải số hoặc dấu chấm nào
    input.value = input.value.replace(/[^0-9.]/g, '');

    // Kiểm tra nếu giá trị nhập vào không hợp lệ, thì xóa giá trị
    if (!/^\d*\.?\d*$/.test(input.value)) {
        input.value = '';
    }
}
