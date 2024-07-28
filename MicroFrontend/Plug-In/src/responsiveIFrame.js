const attachResizeObserver = (id) => {
  const observer = new ResizeObserver((entries) => {
    entries.forEach((entry) => {
      console.log(entry.target.scrollHeight)
      parent.postMessage(entry.target.scrollHeight, '*')
    })
  })

  const el = document.getElementById(id)
  observer.observe(el)
}

export default {
  attachResizeObserver
}
