import axios from 'axios'

const hostURL = process.env.VUE_APP_BASE_URL ? process.env.VUE_APP_BASE_URL : window.location.origin
console.log(process.env.VUE_APP_BASE_URL)

const API = axios.create({
  baseURL: hostURL + '/'
})

export default API
