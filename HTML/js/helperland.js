
$("#accordion").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
      .prev()
      .find("img:last-child")
      .toggleClass("right rotated");
  });


  $("#accordion2").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
      .prev()
      .find("img:last-child")
      .toggleClass("right rotated");
  });