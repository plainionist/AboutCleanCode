<template>
  <div class="responsive-iframe-container" :style="{ paddingTop: iFrameHeight }">
    <iframe
      class="responsive-iframe"
      :src="noCacheUrl"
      :height="iFrameHeight"
      frameborder="0"
      scrolling="no"
      sandbox="allow-scripts allow-same-origin allow-popups allow-downloads"
    />
  </div>
</template>

<script setup>
  import { ref, computed, onMounted, onBeforeUnmount } from 'vue'

  const props = defineProps({
    url: String
  })

  const iFrameHeight = ref('100%')

  const noCacheUrl = computed(() => {
    const url = new URL(props.url)
    url.searchParams.append('cid', Math.round(Math.random() * 10000000))
    return url.toString()
  })

  const resizeIFrame = (e) => {
    const url = new URL(props.url)
    // only react if message comes from the integrated app/plug-in
    if (e.origin === `${url.protocol}//${url.host}`) {
      iFrameHeight.value = e.data + 'px'
    }
  }

  onMounted(() => {
    window.addEventListener('message', resizeIFrame)
  })

  onBeforeUnmount(() => {
    window.removeEventListener('message', resizeIFrame)
  })
</script>

<style scoped>
  .responsive-iframe-container {
    position: relative;
  }

  .responsive-iframe {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    border: 0;
  }
</style>
