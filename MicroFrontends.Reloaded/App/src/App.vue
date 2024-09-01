<template>
  <h1>MyApp</h1>

  <div>Welcome to the micro frontends demo</div>

  <div class="container">
    <FeatureA />
    <FeatureB />
  </div>
</template>

<script setup>
  import { defineAsyncComponent } from 'vue'

  const FeatureA = defineAsyncComponent(() => import('featureA/FeatureA'))
  const FeatureB = defineAsyncComponent(() => import('featureB/FeatureB'))
</script>

<style scoped>
  .container {
    margin-top: 50px;
    display: grid;
    grid-template-columns: 1fr 1fr;
    width: 100%;
    gap: 10px;
  }
</style>
