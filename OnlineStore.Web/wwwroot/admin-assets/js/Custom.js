function formatNumber(input) {
    let value=input.value.replace(/\D/g,'');
    if(value)
    {
        value=value.replace(/\B(?=(\d{3})+(?!\d))/g,',')
    }
    input.value=value;
}