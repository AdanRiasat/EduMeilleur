import { Injectable } from '@angular/core';
import { marked } from 'marked';
import markedKatex from 'marked-katex-extension';
import katex from 'katex';
import { environment } from '../../environments/environment';

const imgBaseURL: string = environment.apiUrl + "/api/Pictures/GetMdImage/"

@Injectable({
  providedIn: 'root'
})
export class MarkdownService {
  constructor() {

    const renderer = new marked.Renderer();

   renderer.image = ({ href, title, text }) => {
      const finalSrc = `${imgBaseURL}${href}?t=${Date.now()}`;
      const alt = text || '';
      const titleAttr = title ? `title="${title}"` : '';
      console.log(finalSrc);
      
      return `<img src="${finalSrc}" alt="${alt}" ${titleAttr} class="img-circle" />`;
    };

    marked.use(
      {
        renderer,
        gfm: true,
        breaks: true
      },
      markedKatex({
        throwOnError: false,
        output: 'html'
      })
    );
  }

  parse(md: string): string {
    return marked.parse(md) as string;
  }
}
