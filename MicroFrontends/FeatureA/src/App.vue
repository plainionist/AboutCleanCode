<template>
  <div id="content-container">
    <h2>Feature A</h2>

    <div v-if="loading">Loading ...</div>
    <div v-else>
      <div>A 1</div>
      <div>A 2</div>
      <div>A 3</div>
      <div>A 4</div>
      <div>A 5</div>
      <div>A 6</div>
      <div>A 7</div>
      <div>A 8</div>
      <div>A 9</div>
      <div>A 10</div>
      <div>A 11</div>
      <div>A 12</div>
      <div>A 13</div>
      <div>A 14</div>
      <div>A 15</div>
      <div>A 16</div>
      <div>A 17</div>
      <div>A 18</div>
      <div>A 19</div>
      <div>A 20</div>
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import { resizeObserver } from 'library'

  const loading = ref(true)

  onMounted(() => {
    resizeObserver.attach('content-container')

    setTimeout(() => {
      loading.value = false
    }, 2000)
  })
</script>

<style scoped>
  #content-container {
    padding: 8px;
    background-color: lightblue
  }
</style>
