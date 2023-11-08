export const onlyNumbers = (e) => {
  const re = /[0-9]+/g;
  if (!re.test(e.key)) {
    e.preventDefault();
  }
};

export const formatDate = (string) => {
  let options = { year: "numeric", month: "long", day: "numeric" };
  let fecha = new Date(string).toLocaleDateString("es-PE", options);
  let hora = new Date(string).toLocaleTimeString();
  return fecha + " | " + hora;
};
