const attach = (id) => {
  const observer = new ResizeObserver((entries) => {
    entries.forEach((entry) => {
      parent.postMessage(entry.target.scrollHeight, '*')
    })
  })

  const el = document.getElementById(id)
  observer.observe(el)
}

export default {
  attach
}
