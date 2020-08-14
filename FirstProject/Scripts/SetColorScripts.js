
function set_color_name(id_color_elem, id_text_elem) {
    var text = document.getElementById(id_text_elem);
    console.log(text);

    var color = document.getElementById(id_color_elem);
    console.log(color);
    text.value = color.value;
}