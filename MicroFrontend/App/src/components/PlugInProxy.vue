<template>
  <div style="border: blue solid 1px">
    <div v-if="api">
      <ResponsiveFrame :api="api" :url="api + '/home'" />
    </div>
    <div v-else style="text-align: center">
      <h3>The plug-in is not configured!</h3>
      <p>If you are interested in this functionality please contact your administrator ;-)</p>
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import ResponsiveFrame from './ResponsiveFrame.vue'

  const api = ref(null)

  onMounted(() => {
    //const { data } = await API.get('/Plugins/Get')
    const data = {
      Help: {
        uri: 'http://localhost:5174'
      }
    }

    api.value = data.Help?.uri
  })
</script>
