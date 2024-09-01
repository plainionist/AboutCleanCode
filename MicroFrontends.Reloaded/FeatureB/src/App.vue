<template>
  <div id="content-container">
    <h2>Feature B</h2>

    <div v-if="loading">Loading ...</div>
    <div v-else>
      <div>B 1</div>
      <div>B 2</div>
      <div>B 3</div>
      <div>B 4</div>
      <div>B 5</div>
      <div>B 6</div>
      <div>B 7</div>
      <div>B 8</div>
      <div>B 9</div>
      <div>B 10</div>
      <div>B 11</div>
      <div>B 12</div>
      <div>B 13</div>
      <div>B 14</div>
      <div>B 15</div>
      <div>B 16</div>
      <div>B 17</div>
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'

  const loading = ref(true)

  onMounted(() => {
    setTimeout(() => {
      loading.value = false
    }, 1000)
  })
</script>

<style scoped>
  #content-container {
    padding: 8px;
    background-color: lightgreen;
  }
</style>
