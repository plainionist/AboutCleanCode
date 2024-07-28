<template>
  <h1>MyApp</h1>

  <div>This paragraph comes from the shell of the App.</div>

  <br />

  <div style="border: blue solid 1px">
    <ResponsiveFrame url="http://localhost:5174" />
  </div>
</template>

<script setup>
  import ResponsiveFrame from './components/ResponsiveFrame.vue'
</script>
