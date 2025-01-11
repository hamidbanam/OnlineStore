function confirmDelete(url,e) {

    e.preventDefault();

    Swal.fire({
        text: "آیا از انجام این عملیات مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بلـــــــــه",
        cancelButtonText: "خیــــــــر"
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = url;
        }
    });

}

function fillPageId(pageId){
    $("#Page").val(pageId);
    $("#filter-search").submit();
}