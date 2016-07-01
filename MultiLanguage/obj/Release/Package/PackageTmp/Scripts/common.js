var filenumber = 1;
$("#addAttach").live("click", function () {
    $("#adAttach").attr("style", "display:block");
    $("#divfileUpload").attr("style", "display:block");
    var count = $("#divfileUpload [type=file]").length;
    if (count < 5) {
        $("<li id=" + count + "><div><div class=\"fleft\"><input type=\"file\"  class=\"fileupload\" name=\"FileUpload\"  id=\"FileUpload" + filenumber + "\" /></div><div class=\"fleft ml10 mt5\"><a href=\"#\" onclick=\"RemoveFileUpload("+count+")\">Remove</a></div></div></li>").prependTo("#FileUploader");

        // declare the rule on this newly created field        
        $("#FileUpload" + filenumber).rules("add", {
            required: true,
            accept: "zip|ZIP|Zip|rar|RAR|Rar|DOC|DOCX|PDF|TXT|RTF|XLS|XLSX|CSV|ODS|XML|ODT|BMP|JPG|JPEG|GIF|PNG|TIFF|doc|docx|pdf|txt|rtf|xls|xlsx|csv|ods|xml|odt|bmp|jpg|jpeg|gif|png|tiff|Doc|Docx|Pdf|Txt|Rtf|Xls|Xlsx|Csv|Ods|Xml|Odt|Bmp|Jpg|Jpeg|Gif|Png|Tiff",
            messages: {
                required: "You must select a file",
                accept: "file format is not valid"
            }
        });
        filenumber++;
        return false;
    }
});

function RemoveFileUpload(e) {
    $('#' + e).remove();
    if (filenumber > 0) {
        var count = $("#divfileUpload [type=file]").length;
        if (count === 1)
            $("#adAttach").attr("style", "display:none");
       
        filenumber--;
    }
    return false;
}